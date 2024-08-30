using AutoMapper;
using GdeIzaci.Data;
using GdeIzaci.Models.Domain;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
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
        private readonly GdeIzaciDBContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IReservationRepository reservationRepository;

        public ReservationController(GdeIzaciDBContext dbContext, IMapper mapper, UserManager<IdentityUser> userManager, IReservationRepository reservationRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.reservationRepository = reservationRepository;
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

            var place = await dbContext.Places.FindAsync(createReservationDto.PlaceID);
            if (place == null)
            {
                return NotFound("Place not found.");
            }

            var reservationDomain = mapper.Map<Reservation>(createReservationDto);

            reservationDomain.Id = Guid.NewGuid();
            reservationDomain.UserId = createReservationDto.UserID;
            reservationDomain.PlaceID = createReservationDto.PlaceID;
            reservationDomain.ReservationDateTime = createReservationDto.ReservationDateTime;

            reservationDomain = await reservationRepository.CreateAsync(reservationDomain);

            var reservationDto = mapper.Map<ReservationDTO>(reservationDomain);

            return CreatedAtAction(nameof(Create), new { id = reservationDto.Id }, reservationDto);
        }


        [HttpGet]
        [Route("check-reservation/{placeId:Guid}")]
        public async Task<IActionResult> CheckReservation([FromRoute] Guid placeId, [FromQuery] Guid userId)
        {
            var reservation = await dbContext.Reservations
                .FirstOrDefaultAsync(r => r.PlaceID == placeId && r.UserId == userId);

            if (reservation != null)
            {
                return Ok(true); // Rezervacija postoji
            }

            return Ok(false); // Rezervacija ne postoji
        }

        [HttpDelete]
        [Route("check-reservation/{placeId:Guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid placeID, [FromQuery] Guid userID)
        {
            try
            {
                var reservation = await dbContext.Reservations
                    .FirstOrDefaultAsync(r => r.PlaceID == placeID && r.UserId == userID);

                if (reservation == null)
                {
                    return NotFound(); // Rezervacija nije pronađena
                }

                dbContext.Reservations.Remove(reservation);
                await dbContext.SaveChangesAsync();

                return NoContent(); // Uspešno obrisano
            }
            catch (Exception ex)
            {
                // Možete dodati logiku za logovanje greške ovde
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
