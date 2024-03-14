using AutoMapper;
using BlogApp.Core.Dtos;
using BlogApp.Core.Dtos.Comment;
using BlogApp.Core.Dtos.Post;
using BlogApp.Core.Dtos.User;
using BlogApp.Core.Models;

namespace BlogApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
