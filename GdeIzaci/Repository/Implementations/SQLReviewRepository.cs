using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Repository.Implementations
{
    public class SQLReviewRepository : IReviewRepository
    {
        private readonly GdeIzaciDBContext dbContext;

        public SQLReviewRepository(GdeIzaciDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Review> CreateAsync(Review enthity)
        {
            await dbContext.Reviews.AddAsync(enthity);
            await dbContext.SaveChangesAsync();
            return enthity;
        }

        public async Task<Review?> DeleteAsync(Guid id)
        {
            var existingReview = await dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewID == id);
            if (existingReview != null)
            {
                dbContext.Reviews.Remove(existingReview);
                await dbContext.SaveChangesAsync();
                return existingReview;
            }
            return null;
        }

        public async Task<List<Review>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var reviews = dbContext.Reviews.AsQueryable();

            //Filtering

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("ReviewID", StringComparison.OrdinalIgnoreCase))
                {
                    reviews = reviews.Where(x => x.ReviewID == Guid.Parse(filterQuery));
                }

            }
            return await reviews.ToListAsync();
        }

        public async Task<Review?> GetAverageAsync(Guid placeId)
        {
            var reviews = await dbContext.Reviews.Where(r => r.PlaceID == placeId).ToListAsync();

            if (reviews.Count == 0)
                return null;

            var averageRating = reviews.Average(r => r.numberOfStars);

            // Napravi novi Review objekat koji će sadržati prosečnu ocenu.
            var reviewWithAverage = new Review
            {
                PlaceID = placeId,
                numberOfStars = averageRating
            };

            return reviewWithAverage;
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            return await dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewID == id);
        }

        public async Task<Review?> GetByPlaceIdUserIdAsync(Guid placeId, Guid userId)
        {
            return await dbContext.Reviews.FirstOrDefaultAsync(x => x.PlaceID == placeId && x.UserID==userId);
        }

        public async Task<Review?> UpdateAsync(Guid id, Review review)
        {

            var existingReview = await dbContext.Reviews.FirstOrDefaultAsync(x => x.PlaceID == id);
            if (existingReview == null)
            {
                // Ako entitet sa ovim ID-om ne postoji, vratite null
                return null;
            }

            // Ažurirajte postojeći entitet sa novim vrednostima
            existingReview.numberOfStars = review.numberOfStars;
            await dbContext.SaveChangesAsync();
          
            return existingReview;
        }

    }

}
