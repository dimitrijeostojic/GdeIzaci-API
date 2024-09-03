using GdeIzaci.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Repository.Interfaces
{
    public interface IPlaceItemRepository
    {
        Task<List<PlaceItem>> GetAllAsync();
    }
}
