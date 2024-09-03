using GdeIzaci.Models.DTO;

namespace GdeIzaci.Services.Interfaces
{
    public interface IPlaceService
    {
        Task<List<PlaceDTO>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize);
        Task<PlaceDTO> GetByIdAsync(Guid id);
        Task<PlaceDTO> CreateAsync(AddPlaceRequestDto addPlaceRequestDto, Guid userId);
        Task<PlaceDTO> UpdateAsync(Guid id, UpdatePlaceRequestDto updatePlaceRequestDto, Guid userId, string role);
        Task<PlaceDTO> DeleteAsync(Guid id, Guid userId, string role);
    }
}
