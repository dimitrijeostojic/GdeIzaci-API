using AutoMapper;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;

namespace GdeIzaci.Services.Implementations
{
    public class PlaceService : IPlaceService
    {

        private readonly IPlaceRepository placeRepository;
        private readonly IMapper mapper;

        public PlaceService(IPlaceRepository placeRepository, IMapper mapper)
        {
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }

        public async Task<PlaceDTO> CreateAsync(AddPlaceRequestDto addPlaceRequestDto, Guid userId)
        {
            var placeDomain = mapper.Map<Place>(addPlaceRequestDto);
            placeDomain.UserCreatedID = userId;
            placeDomain = await placeRepository.CreateAsync(placeDomain);
            return mapper.Map<PlaceDTO>(placeDomain);
        }

        public async Task<PlaceDTO> DeleteAsync(Guid id, Guid userId, string role)
        {
            var existingPlace = await placeRepository.GetByIdAsync(id);

            if (role == "Admin" || (existingPlace != null && existingPlace.UserCreatedID == userId))
            {
                existingPlace = await placeRepository.DeleteAsync(id);
                return mapper.Map<PlaceDTO>(existingPlace);
            }

            return null;
        }

        public async Task<List<PlaceDTO>> GetAllAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var places = await placeRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            return mapper.Map<List<PlaceDTO>>(places);
        }

        public async Task<PlaceDTO> GetByIdAsync(Guid id)
        {
            var existingPlace = await placeRepository.GetByIdAsync(id);
            if (existingPlace == null)
            {
                return null;
            }
            return mapper.Map<PlaceDTO>(existingPlace);
        }

        public async Task<PlaceDTO> UpdateAsync(Guid id, UpdatePlaceRequestDto updatePlaceRequestDto, Guid userId, string role)
        {
            var placeDomain = mapper.Map<Place>(updatePlaceRequestDto);
            var existingPlace = await placeRepository.GetByIdAsync(id);

            if (role == "Admin" || (existingPlace != null && existingPlace.UserCreatedID == userId))
            {
                existingPlace = await placeRepository.UpdateAsync(id, placeDomain);
                return mapper.Map<PlaceDTO>(existingPlace);
            }

            return null;
        }
    }
}
