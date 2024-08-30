using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Repository.Implementations
{
    public class SQLPlaceItemRepository : IPlaceItemRepository
    {
        private readonly GdeIzaciDBContext dbContext;

        public SQLPlaceItemRepository(GdeIzaciDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<PlaceItem> CreateAsync(PlaceItem enthity)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceItem?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PlaceItem>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var placeItems = dbContext.PlaceItems.AsQueryable();

            return await placeItems.ToListAsync();
        }

        public async Task<PlaceItem?> GetByIdAsync(Guid id)
        {
            return await dbContext.PlaceItems.FirstOrDefaultAsync(x => x.PlaceItemID == id);
        }

        public async Task<PlaceItem?> UpdateAsync(Guid id, PlaceItem enthity)
        {
            var existingPlaceItem = await dbContext.PlaceItems.FirstOrDefaultAsync(x => x.PlaceItemID == id);
            if (existingPlaceItem == null)
            {
                return null;
            }
            existingPlaceItem.NumberOfPlacesCurrentlyOfThisType = enthity.NumberOfPlacesCurrentlyOfThisType;
            await dbContext.SaveChangesAsync();
            return existingPlaceItem;
        }
    }
}
