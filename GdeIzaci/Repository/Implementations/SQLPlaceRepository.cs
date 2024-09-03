using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Repository.Implementations
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

        public async Task<Place> DeleteAsync(Guid id)
        {
            var existingPlace = await dbContext.Places.FirstOrDefaultAsync(x => x.PlaceID == id);
            if (existingPlace != null)
            {
                dbContext.Places.Remove(existingPlace);
                await dbContext.SaveChangesAsync();
                return existingPlace;
            }
            return null;

        }

        public async Task<List<Place>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var places = dbContext.Places.Include("PlaceItem").AsQueryable();

            //Filtering

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    places = places.Where(x => x.Name.Contains(filterQuery));
                }
                if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    places = places.Where(x => x.Description.Contains(filterQuery));
                }
                if (filterOn.Equals("PlaceItem", StringComparison.OrdinalIgnoreCase))
                {
                    places = places.Where(x => x.PlaceItem.Name.Contains(filterQuery));
                }
            }

            //Sorting

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    places = (bool)isAscending ? places.OrderBy(x => x.Name) : places.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    places = (bool)isAscending ? places.OrderBy(x => x.Price) : places.OrderByDescending(x => x.Price);
                }
            }

            //Pagination

            var skipResults = (pageNumber - 1) * pageSize;

            return await places.Skip(skipResults).Take(pageSize).ToListAsync();

        }


        public async Task<Place?> GetByIdAsync(Guid id)
        {
            return await dbContext.Places.Include("PlaceItem").FirstOrDefaultAsync(x => x.PlaceID == id);
        }

        public async Task<List<Place>> GetPlacesForUserAsync(Guid userId)
        {
            var places = await dbContext.Reservations.Where(r => r.UserId == userId).Include(r => r.Place).Select(r => r.Place).ToListAsync();
            return places;
        }

        public async Task<Place?> UpdateAsync(Guid id, Place place)
        {
            var existingPlace = await dbContext.Places.FirstOrDefaultAsync(x => x.PlaceID == id);
            if (existingPlace == null)
            {
                return null;
            }
            existingPlace.Name = place.Name;
            existingPlace.Description = place.Description;
            existingPlace.Location = place.Location;
            existingPlace.Date=place.Date;
            existingPlace.Price = place.Price;
            existingPlace.Photo = place.Photo;
            existingPlace.PlaceItemID = place.PlaceItemID;
            await dbContext.SaveChangesAsync();
            return existingPlace;

        }
    }
}
