using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly GdeIzaciDBContext dbContext;
        private readonly IPlaceRepository placeRepository;
        private readonly IMapper mapper;

        public PlaceController(GdeIzaciDBContext dbContext, IPlaceRepository placeRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.placeRepository = placeRepository;
            this.mapper = mapper;
        }

        //GET: https://localhost:7801/api/places
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var places = await placeRepository.GetAllAsync();
            //convert to Dto
            var placesDto = mapper.Map<List<PlaceDTO>>(places);

            return Ok(placesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPlaceRequestDto addPlaceRequestDto)
        {
            //convert to domain
            var placeDomain = mapper.Map<Place>(addPlaceRequestDto);

            placeDomain = await placeRepository.CreateAsync(placeDomain);

            //convert to dto
            var placeDto = mapper.Map<PlaceDTO>(placeDomain);

            return Ok(placeDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var existingPlace = await placeRepository.GetByIdAsync(id);
            if (existingPlace == null)
            {
                return BadRequest("Place doesn't exist");
            }
            //Convert to Dto
            var placeDTO = mapper.Map<PlaceDTO>(existingPlace);
            return Ok(placeDTO);
        }
    }
}
