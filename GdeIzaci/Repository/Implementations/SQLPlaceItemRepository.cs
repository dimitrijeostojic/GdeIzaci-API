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

        public async Task<List<PlaceItem>> GetAllAsync()
        {
            return await dbContext.PlaceItems.ToListAsync();
        }

       
    }
}
