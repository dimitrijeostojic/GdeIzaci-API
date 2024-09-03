using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Repository.Implementations
{
    public class SQLReservationRepository : IReservationRepository
    {
        private readonly GdeIzaciDBContext dbContext;

        public SQLReservationRepository(GdeIzaciDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation?> DeleteAsync(Guid id)
        {
            var existingReservation = await dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == id);
            if (existingReservation != null)
            {
                dbContext.Reservations.Remove(existingReservation);
                await dbContext.SaveChangesAsync();
                return existingReservation;
            }
            return null;
        }

        public async Task<Reservation?> GetByIdAsync(Guid placeID,Guid userID)
        {
            return await dbContext.Reservations
                .FirstOrDefaultAsync(r => r.PlaceID == placeID && r.UserId == userID);
        }
    }
}
