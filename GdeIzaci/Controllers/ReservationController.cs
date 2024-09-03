using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly UserManager<IdentityUser> userManager;

        public ReservationController(IReservationService reservationService, UserManager<IdentityUser> userManager)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationDTO createReservationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("User must be logged in to make a reservation.");
            }

            try
            {
                var reservationDto = await reservationService.CreateReservationAsync(createReservationDto);
                return CreatedAtAction(nameof(Create), new { id = reservationDto.Id }, reservationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("check-reservation/{placeId:Guid}")]
        public async Task<IActionResult> CheckReservation([FromRoute] Guid placeID, [FromQuery] Guid userID)
        {
            var exists = await reservationService.CheckReservationAsync(placeID, userID);
            return Ok(exists);
        }

        [HttpDelete]
        [Route("check-reservation/{placeId:Guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid placeID, [FromQuery] Guid userID)
        {
            try
            {
                var reservationDto = await reservationService.DeleteReservationAsync(placeID, userID);
                return Ok(reservationDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
