using AutoMapper;
using DTO.I;
using Entities;

namespace Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TeamInsertDTO, Team>();
            CreateMap<GameInsertDTO, Game>();
        }
    }
}