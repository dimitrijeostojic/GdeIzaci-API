using GdeIzaci.Models.DTO;

namespace GdeIzaci.Services.Interfaces
{
    public interface IPlaceItemService
    {
        Task<List<PlaceItemDTO>> GetAllAsync();
    }
}
