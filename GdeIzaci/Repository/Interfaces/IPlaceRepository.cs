using GdeIzaci.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Repository.Interfaces
{
    public interface IPlaceRepository
    {
        Task<Place> CreateAsync(Place enthity);
        Task<Place?> GetByIdAsync(Guid id);
        Task<Place?> DeleteAsync(Guid id);
        Task<List<Place>> GetAllAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000);
        Task<Place?> UpdateAsync(Guid id, Place enthity);

    }
}
