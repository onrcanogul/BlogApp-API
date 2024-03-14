using AutoMapper;
using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Comment;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Models;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;
using BlogApp.Core.UnitOfWork;
using BlogApp.Service.Exceptions;

namespace BlogApp.Service.Services
{
    public class CommentService : Service<Comment,CommentDto> , ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork, ICommentRepository commentRepository, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetCommentsByUser(string id)
        {
            var comments = await _commentRepository.GetCommentsByUser(id);
            if (comments.Count == 0)
            {
                throw new NotFoundException("data is not found");
            }
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return CustomResponseDto<List<CommentDto>>.Success(200,commentsDto);
        }

        public async Task<CustomResponseDto<List<CommentDto>>> GetCommentsByPost(int id)
        {
            var comments = await _commentRepository.GetCommentsByPost(id);
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return CustomResponseDto<List<CommentDto>>.Success(200,commentsDto);
        }
        public async Task<CustomResponseDto<NoContentDto>> Delete(int id)
        {
            await _commentRepository.Delete(id);
            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> Update(UpdateCommentDto updateCommentDto)
        {
            await _commentRepository.Update(updateCommentDto);
            await _unitOfWork.CommitAsync();


            return CustomResponseDto<NoContentDto>.Success(204);
        }




    }
}
