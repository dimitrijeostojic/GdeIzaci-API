using GdeIzaci.Models.Domain;

namespace GdeIzaci.Repository
{
    public interface IPlaceRepository
    {
        Task<Place> CreateAsync(Place place);
        Task<List<Place>> GetAllAsync();
        Task<Place?> GetByIdAsync(Guid id);
    }
}
