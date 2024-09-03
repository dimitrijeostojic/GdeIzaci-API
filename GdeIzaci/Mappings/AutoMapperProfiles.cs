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
            CreateMap<UpdatePlaceRequestDto, Place>().ReverseMap();
            CreateMap<PlaceItem, PlaceItemDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Review, AddReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
            CreateMap<Reservation, CreateReservationDTO>().ReverseMap();

        }
    }
}
