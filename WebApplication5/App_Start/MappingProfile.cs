using AutoMapper;
using WebApplication5.DataTransferObjects;
using WebApplication5.Models;

namespace WebApplication5
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Player, PlayerDto>();
            Mapper.CreateMap<PlayerDto, Player>();
            Mapper.CreateMap<Country, CountryDto>();
            Mapper.CreateMap<HeadToHead, HeadToHeadDto>();
        }
    }
}