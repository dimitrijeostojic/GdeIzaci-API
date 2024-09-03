    using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Globalization;
using System.Security.Claims;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService placeService;
        private readonly UserManager<IdentityUser> userManager;

        public PlaceController(IPlaceService placeService, UserManager<IdentityUser> userManager)
        {
            this.placeService = placeService;
            this.userManager = userManager;
        }

        //GET: https://localhost:7801/api/places
        [HttpGet]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var placesDto = await placeService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            return Ok(placesDto);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] AddPlaceRequestDto addPlaceRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("User isn't logged in, please login");
            }

            var placeDto = await placeService.CreateAsync(addPlaceRequestDto, Guid.Parse(currentUser.Id));
            return CreatedAtAction(nameof(GetById), new { id = placeDto.PlaceID }, placeDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var placeDTO = await placeService.GetByIdAsync(id);
            if (placeDTO == null)
            {
                return BadRequest("Place doesn't exist");
            }
            return Ok(placeDTO);
        }

        //DELETE: localhost:8081/api/place/3
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var role = User.IsInRole("Admin") ? "Admin" : "Manager";
            var placeDto = await placeService.DeleteAsync(id, Guid.Parse(currentUser.Id), role);

            if (placeDto == null)
            {
                return BadRequest("This place doesn't exist or you are not allowed to delete this object");
            }
            return Ok(placeDto);
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
            var role = User.IsInRole("Admin") ? "Admin" : "Manager";
            var placeDto = await placeService.UpdateAsync(id, updatePlaceRequestDto, Guid.Parse(currentUser.Id), role);

            if (placeDto == null)
            {
                return BadRequest("This place doesn't exist or you are not allowed to update this object");
            }
            return Ok(placeDto);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles="RegularUser")]
        public async Task<IActionResult> GetPlacesForUser(Guid userId)
        {
            // Prvo dohvatamo rezervacije korisnika i radimo join sa Place tabelom
            var places = await placeService.GetPlacesForUserAsync(userId);
          
            return Ok(places);
        }
    }
}
