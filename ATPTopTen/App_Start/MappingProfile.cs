using ATPTopTen.DataTransferObjects;
using ATPTopTen.Models;
using AutoMapper;

namespace ATPTopTen
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