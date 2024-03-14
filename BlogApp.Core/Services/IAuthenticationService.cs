using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Token;
using BlogApp.Core.Dtos.User;

namespace BlogApp.Core.Services
{
    public interface IAuthenticationService
    {
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto);
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(RefreshTokenDto refreshTokenDto);
    }
}
