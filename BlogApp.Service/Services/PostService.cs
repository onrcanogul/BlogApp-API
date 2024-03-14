using AutoMapper;
using Azure;
using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Dtos.Post;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;
using BlogApp.Core.UnitOfWork;

namespace BlogApp.Service.Services
{
    public class PostService : Service<Post,PostDto> , IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IGenericRepository<Post> repository, IUnitOfWork unitOfWork, IMapper mapper, IPostRepository postRepository) : base(repository, unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task <CustomResponseDto<List<PostDto>>> GetPostByUser(GetByIdDto userDto)
        {
            var posts = await _postRepository.GetPostByUser(userDto);
           var postsDto = _mapper.Map<List<PostDto>>(posts);


            var response = CustomResponseDto<List<PostDto>>.Success(200, postsDto);
            return response;
        }

        public async Task<CustomResponseDto<IEnumerable<PostDto>>> GetAll()
        {
            var posts =await _postRepository.GetAllAsync();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            
            var response = CustomResponseDto<IEnumerable<PostDto>>.Success(200, postsDto);
            

            return response;
        }

        public async Task<CustomResponseDto<PostDto>> GetPostById(int id)
        {
            var post = await _postRepository.GetById(id);
            var postDto = _mapper.Map<PostDto>(post);

            var response = CustomResponseDto<PostDto>.Success(200, postDto);
            

            return response;
        }

        public async Task<CustomResponseDto<NoContentDto>> Update(UpdatePostDto updatePostDto, int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            post.Title = updatePostDto.Title;
            post.Description = updatePostDto.Description;
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> Delete(int id)
        {
            await _postRepository.Delete(id);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }
        
    }
}
