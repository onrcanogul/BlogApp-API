using System.Security.Claims;
using AutoMapper;
using BlogApp.API.Filters;
using BlogApp.Core.Dtos.Id;
using BlogApp.Core.Dtos.Post;
using BlogApp.Core.Models;
using BlogApp.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class PostController : CustomBaseController
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return ActionResultInstance(await _postService.GetAll());
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(NotFoundFilter<Post,PostDto>))]
        public async Task<IActionResult> GetById(int id)
        {
            return ActionResultInstance(await _postService.GetPostById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetPostByUser(GetByIdDto userDto)
        {
            return ActionResultInstance(await _postService.GetPostByUser(userDto));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(PostDto postDto)
        {
            postDto.UserId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return ActionResultInstance(await _postService.CreateAsync(postDto));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ServiceFilter(typeof(NotFoundFilter<Post, PostDto>))]
        public async Task<IActionResult> Update(UpdatePostDto updatePostDto, int id)
        {
            if(updatePostDto.Id == id)
            {
                return ActionResultInstance(await _postService.Update(updatePostDto, updatePostDto.Id));
            }
            return NotFound();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ServiceFilter(typeof(NotFoundFilter<Post, PostDto>))]
        public async Task<IActionResult> Delete(int id)
        {
            return ActionResultInstance(await _postService.Delete(id));
        }
    }
}
