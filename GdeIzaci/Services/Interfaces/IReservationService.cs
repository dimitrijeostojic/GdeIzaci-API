using GdeIzaci.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationDTO> CreateReservationAsync(CreateReservationDTO createReservationDto);
        Task<ReservationCheckResultDTO> CheckReservationAsync(Guid placeId, Guid userId);
        Task<ReservationDTO> DeleteReservationAsync(Guid placeId, Guid userId);
    }
}
