using GdeIzaci.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Repository.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> CreateAsync(Reservation reservation);
        Task<Reservation?> GetByIdAsync(Guid placeID,Guid userID);
        Task<Reservation?> DeleteAsync(Guid id);
    }
}
