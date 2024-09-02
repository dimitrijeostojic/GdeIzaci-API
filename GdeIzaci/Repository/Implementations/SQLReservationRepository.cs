using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        public Task<Reservation?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reservation>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> GetAverageAsync(Guid placeId)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> GetByPlaceIdUserIdAsync(Guid placeId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> UpdateAsync(Guid id, Reservation enthity)
        {
            throw new NotImplementedException();
        }
    }
}
