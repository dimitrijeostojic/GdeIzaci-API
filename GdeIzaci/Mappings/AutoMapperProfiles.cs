using AutoMapper;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;

namespace GdeIzaci.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Place, PlaceDTO>().ReverseMap();
            CreateMap<AddPlaceRequestDto, Place>().ReverseMap();
        }
    }
}
