using AutoMapper;
using Azure;
using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.User;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;
using BlogApp.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Service.Services
{
    public class UserService : Service<AppUser,AppUserDto> , IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public UserService(IGenericRepository<AppUser> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper, UserManager<AppUser> userManager) : base(repository, unitOfWork,mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CustomResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new AppUser()
            {
                UserName = createUserDto.UserName, Email = createUserDto.Email
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<AppUserDto>.Fail(400,new ErrorDto(errors,true));
            }
            var response = CustomResponseDto<AppUserDto>.Success(200, _mapper.Map<AppUserDto>(user));
            
            return response;
        }

        public async Task<CustomResponseDto<AppUserDto>> GetUserFromPost(int postId)
        {
            var user = await _userRepository.GetUserFromPost(postId);
            var userDto = _mapper.Map<AppUserDto>(user);

            return CustomResponseDto<AppUserDto>.Success(200, userDto);
        }

        public async Task<CustomResponseDto<AppUserDto>> GetUserFromComment(int commentId)
        {
            var user = await _userRepository.GetUserFromComment(commentId);
            var userDto = _mapper.Map<AppUserDto>(user);
            var response = CustomResponseDto<AppUserDto>.Success(200, userDto);
            
            return response;
        }
    }
}
