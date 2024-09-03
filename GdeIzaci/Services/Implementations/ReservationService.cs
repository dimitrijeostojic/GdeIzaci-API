using AutoMapper;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Services.Implementations
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository reservationRepository;
        private readonly IPlaceRepository placeRepository;
        private readonly IMapper mapper;

        public ReservationService(IReservationRepository reservationRepository, IPlaceRepository placeRepository, IMapper mapper)
        {
            this.reservationRepository = reservationRepository;
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }


        public async Task<bool> CheckReservationAsync(Guid placeId, Guid userId)
        {
            var reservation = await reservationRepository.GetByIdAsync(placeId, userId);
            return reservation != null;
        }

        public async Task<ReservationDTO> CreateReservationAsync(CreateReservationDTO createReservationDto)
        {
            var place = await placeRepository.GetByIdAsync(createReservationDto.PlaceID);
            if (place == null)
            {
                throw new Exception("Place not found.");
            }

            var reservationDomain = mapper.Map<Reservation>(createReservationDto);
            reservationDomain.Id = Guid.NewGuid();
            reservationDomain.UserId = createReservationDto.UserID;
            reservationDomain.PlaceID = createReservationDto.PlaceID;
            reservationDomain.ReservationDateTime = createReservationDto.ReservationDateTime;

            var createdReservation = await reservationRepository.CreateAsync(reservationDomain);

            return mapper.Map<ReservationDTO>(createdReservation);
        }

        public async Task<ReservationDTO> DeleteReservationAsync(Guid placeId, Guid userId)
        {
            var reservation = await reservationRepository.GetByIdAsync(placeId, userId);
            if (reservation == null)
            {
                throw new Exception("Reservation not found.");
            }

            var deletedReservation = await reservationRepository.DeleteAsync(reservation.Id);
            return mapper.Map<ReservationDTO>(deletedReservation);
        }
    }
}
