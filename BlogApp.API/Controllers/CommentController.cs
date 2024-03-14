using AutoMapper;
using BlogApp.API.Filters;
using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Comment;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Dtos.Post;
using BlogApp.Core.Models;
using BlogApp.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : CustomBaseController
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(CommentDto commentDto)
        {
            commentDto.UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return ActionResultInstance(await _commentService.CreateAsync(commentDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _commentService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentByUser(string id)
        {
            return ActionResultInstance(await _commentService.GetCommentsByUser(id));
        }
        [HttpGet("{id}")]

        [ServiceFilter(typeof(NotFoundFilter<Comment, NoContentDto>))]
        public async Task<IActionResult> GetCommentByPost(int id)
        {
            return ActionResultInstance(await _commentService.GetCommentsByPost(id));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(UpdateCommentDto updateCommentDto)
        {
            if (User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value == updateCommentDto.UserId)
            {
                return ActionResultInstance(await _commentService.Update(updateCommentDto));
            }
            return ActionResultInstance(CustomResponseDto<NoContentDto>.Fail(404, "u cant change this comment",true));
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ServiceFilter(typeof(NotFoundFilter<Comment, CommentDto>))]
        public async Task<IActionResult> Delete(int id)
        {
                return ActionResultInstance(await _commentService.Delete(id));
        }
        }
    }

