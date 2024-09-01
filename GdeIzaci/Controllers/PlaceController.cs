using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Globalization;
using System.Security.Claims;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly GdeIzaciDBContext dbContext;
        private readonly IPlaceRepository placeRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IPlaceItemRepository placeItemRepository;

        public PlaceController(GdeIzaciDBContext dbContext, IPlaceRepository placeRepository, IMapper mapper, UserManager<IdentityUser> userManager, IPlaceItemRepository placeItemRepository)
        {
            this.dbContext = dbContext;
            this.placeRepository = placeRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.placeItemRepository = placeItemRepository;
        }

        //GET: https://localhost:7801/api/places
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var places = await placeRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            //convert to Dto
            var placesDto = mapper.Map<List<PlaceDTO>>(places);

            return Ok(placesDto);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] AddPlaceRequestDto addPlaceRequestDto)
            {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("User isn't logged in, please login");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //convert to domain
            var placeDomain = mapper.Map<Place>(addPlaceRequestDto);

            placeDomain.UserCreatedID = Guid.Parse(currentUser.Id);
            placeDomain.PlaceItemID = addPlaceRequestDto.PlaceItemID;

            placeDomain = await placeRepository.CreateAsync(placeDomain);

            //update number of places of that type in placeItem

            var placeItem = await placeItemRepository.GetByIdAsync(placeDomain.PlaceItemID);
            placeItem.NumberOfPlacesCurrentlyOfThisType++;

            placeItem = await placeItemRepository.UpdateAsync(placeItem.PlaceItemID, placeItem);

            //convert to dto
            var placeDto = mapper.Map<PlaceDTO>(placeDomain);
            return CreatedAtAction(nameof(GetById), new { id = placeDomain.PlaceID }, placeDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
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

        //DELETE: localhost:8081/api/place/3
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (User.IsInRole("Admin"))
            {
                var existingPlace = await placeRepository.DeleteAsync(id);

                if (existingPlace == null)
                {
                    return BadRequest("This place doesn't exist");
                }

                //update number of places of that type in placeItem
                var placeItem = await placeItemRepository.GetByIdAsync(existingPlace.PlaceItemID);
                placeItem.NumberOfPlacesCurrentlyOfThisType--;
                placeItem = await placeItemRepository.UpdateAsync(placeItem.PlaceItemID, placeItem);
                //Convert to dto
                var placeDTO = mapper.Map<PlaceDTO>(existingPlace);
                return Ok(placeDTO);
            }
            if (User.IsInRole("Manager"))
            {
                var existingPlace = await placeRepository.GetByIdAsync(id);
                if (existingPlace != null && existingPlace.UserCreatedID == Guid.Parse(currentUser.Id))
                {
                    existingPlace = await placeRepository.DeleteAsync(id);
                    //update number of places of that type in placeItem
                    var placeItem = await placeItemRepository.GetByIdAsync(existingPlace.PlaceItemID);
                    placeItem.NumberOfPlacesCurrentlyOfThisType--;
                    placeItem = await placeItemRepository.UpdateAsync(placeItem.PlaceItemID, placeItem);
                    //convert to dto
                    var placeDTO = mapper.Map<PlaceDTO>(existingPlace);
                    return Ok(placeDTO);
                }
                return BadRequest("This place doesn't exist or you are not allowed to delete this object");
            }
            return Unauthorized("You don't have permission to delete objects");
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePlaceRequestDto updatePlaceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUser = await userManager.GetUserAsync(User);
            var placeDomain = mapper.Map<Place>(updatePlaceRequestDto);
            if (User.IsInRole("Admin"))
            {
                //convert to domain
                var existingPlace = await placeRepository.UpdateAsync(id, placeDomain);
                if (existingPlace == null)
                {
                    return BadRequest("This place doesn't exist");
                }
                //convert to dto
                var placeDto = mapper.Map<PlaceDTO>(existingPlace);
                return Ok(placeDto);
            }
            if (User.IsInRole("Manager"))
            {
                var existingPlace = await placeRepository.GetByIdAsync(id);
                if (existingPlace != null && existingPlace.UserCreatedID == Guid.Parse(currentUser.Id))
                {
                    existingPlace = await placeRepository.UpdateAsync(id, placeDomain);
                    var placeDto = mapper.Map<PlaceDTO>(existingPlace);
                    return Ok(placeDto);

                }
                return BadRequest("This place doesn't exist or you are not allowed to update this object");
            }
            return Unauthorized("You don't have permission to update objects");
        }
    }
}
