using AutoMapper;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;

namespace GdeIzaci.Services.Implementations
{
    public class PlaceItemService : IPlaceItemService
    {
        private readonly IPlaceItemRepository placeItemRepository;
        private readonly IMapper mapper;

        public PlaceItemService(IPlaceItemRepository placeItemRepository, IMapper mapper)
        {
            this.placeItemRepository = placeItemRepository;
            this.mapper = mapper;
        }

        public async Task<List<PlaceItemDTO>> GetAllAsync()
        {
            var placeItems = await placeItemRepository.GetAllAsync();
            return mapper.Map<List<PlaceItemDTO>>(placeItems);
        }
    }
}
