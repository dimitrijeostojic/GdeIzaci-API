using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceItemController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly GdeIzaciDBContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IPlaceItemRepository placeItemRepository;

        public PlaceItemController(IMapper mapper, GdeIzaciDBContext dbContext, UserManager<IdentityUser> userManager, IPlaceItemRepository placeItemRepository)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.placeItemRepository = placeItemRepository;
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, UpdatePlaceItemRequestDTO updatePlaceItemRequestDto)
        {
            //convert to domain
            var placeItemDomain = mapper.Map<PlaceItem>(updatePlaceItemRequestDto);
            var existingPlaceItem = await placeItemRepository.UpdateAsync(id, placeItemDomain);
            if (existingPlaceItem == null)
            {
                return BadRequest("This placeItem doesn't exist");
            }
            //convert to dto
            var placeItemDto = mapper.Map<PlaceItemDTO>(existingPlaceItem);
            return Ok(placeItemDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var placeItems = await placeItemRepository.GetAllAsync(filterOn, filterQuery,sortBy,isAscending,pageNumber,pageSize);

            //convert to Dto
            var placeItemsDto = mapper.Map<List<PlaceItemDTO>>(placeItems);

            return Ok(placeItemsDto);

        }
    }
}
