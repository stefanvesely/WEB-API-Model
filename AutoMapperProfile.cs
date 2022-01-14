using AutoMapper;
using try5000rpg.DTOs.Characters;
using try5000rpg.Models; 

namespace try5000rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Characters, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Characters>();
        }
    }
}