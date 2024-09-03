using GdeIzaci.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Repository.Interfaces
{
    public interface IReviewRepository
    {
        Task<Review> CreateAsync(Review enthity);
        Task<List<Review>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000);
        Task<Review?> GetByIdAsync(Guid id);
        Task<Review?> DeleteAsync(Guid id);
        Task<Review?> UpdateAsync(Guid id, Review enthity);
        Task<Review?> GetAverageAsync(Guid placeId);
        Task<Review?> GetByPlaceIdUserIdAsync(Guid placeId, Guid userId);
    }
}
