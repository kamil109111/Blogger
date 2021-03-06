using Application.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Dto.Dapper
{
    public class DapperCreatePostDto : IMap
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DapperCreatePostDto, Post>();
        }
    }
}
