using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Token;
using BlogApp.Core.Dtos.User;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;
using BlogApp.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(400,"email or password is wrong",true);
            }

            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
            {
                return CustomResponseDto<TokenDto>.Fail(400, "email or password is wrong", true);
            }

            var token = _tokenService.CreateToken(user);
            var userRefreshToken =
                await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefreshTokenService.CreateAsync(new UserRefreshToken()
                    { UserId = user.Id, Token = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Success(200,token);
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var existRefreshToken =
                await _userRefreshTokenService.Where(x => x.Token == refreshTokenDto.Token).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404,"Refresh token is not found", true);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404,"UserId is not found",true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Token = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TokenDto>.Success(200,tokenDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var existRefreshToken =
               await  _userRefreshTokenService.Where(x => x.Token == refreshTokenDto.Token).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(200,"Refresh token not found", true);
            }

            _userRefreshTokenService.Delete(existRefreshToken);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }
}
