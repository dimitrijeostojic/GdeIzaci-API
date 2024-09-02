using GdeIzaci.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Repository.Interfaces
{
    public interface IRepository<TEnthity> where TEnthity : class
    {
        Task<TEnthity> CreateAsync(TEnthity enthity);
        Task<List<TEnthity>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000);
        Task<TEnthity?> GetByIdAsync(Guid id);
        Task<TEnthity?> DeleteAsync(Guid id);
        Task<TEnthity?> UpdateAsync(Guid id, TEnthity enthity);
        Task<TEnthity?> GetAverageAsync(Guid placeId);
        Task<TEnthity?> GetByPlaceIdUserIdAsync(Guid placeId, Guid userId);

    }
}
