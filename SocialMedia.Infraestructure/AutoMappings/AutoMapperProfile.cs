using AutoMapper;
using SocialMedia.Core.DTOs.PostDTOs;
using SocialMedia.Core.Entities.PostEntity;

namespace SocialMedia.Infraestructure.AutoMappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
        }
    }
}
