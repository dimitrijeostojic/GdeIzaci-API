using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Repository
{
    public class SQLPlaceRepository : IPlaceRepository
    {
        private readonly GdeIzaciDBContext dbContext;

        public SQLPlaceRepository(GdeIzaciDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Place> CreateAsync(Place place)
        {
            await dbContext.Places.AddAsync(place);
            await dbContext.SaveChangesAsync();
            return place;
        }

        public async Task<List<Place>> GetAllAsync()
        {
            return await dbContext.Places.ToListAsync();
        }

        public async Task<Place?> GetByIdAsync(Guid id)
        {
            return await dbContext.Places.FirstOrDefaultAsync(x => x.PlaceID == id);

        }
    }
}
