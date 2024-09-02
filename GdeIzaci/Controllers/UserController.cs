using GdeIzaci.Data;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly GdeIzaciDBContext dbContext;
        private readonly IPlaceRepository placeRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IReservationRepository reservationRepository;

        public UserController(UserManager<IdentityUser> userManager, GdeIzaciDBContext dbContext, IPlaceRepository placeRepository, IReviewRepository reviewRepository, IReservationRepository reservationRepository)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.placeRepository = placeRepository;
            this.reviewRepository = reviewRepository;
            this.reservationRepository = reservationRepository;
        }
   

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var objectCount = await dbContext.Places.CountAsync(p => p.UserCreatedID == id);

            return Ok(new
            {
                user.UserName,
                numberOfObjects=objectCount,
                userID = user.Id
            });
        }

        [HttpGet]
        [Route("managers")]
        [Authorize(Roles = "Manager, RegularUser")]
        public async Task<IActionResult> GetManagers()
        {
            // Dobavljanje korisnika sa ulogom "Manager"
            var usersInManagerRole = await userManager.GetUsersInRoleAsync("Manager");

            var managers = new List<object>();

            // Dobavljanje uloga za svakog korisnika
            foreach (var user in usersInManagerRole)
            {
                var roles = await userManager.GetRolesAsync(user);
                managers.Add(new
                {
                    user.UserName,
                    user.Email,
                    NumberOfObjects = dbContext.Places.Count(p => p.UserCreatedID == new Guid(user.Id)),
                    Roles = roles, // Vraća uloge korisnika
                    user.Id
                });
            }

            return Ok(managers);
        }

        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllNonAdminUsers()
        {
            // Dobavljanje svih korisnika iz baze
            var allUsers = await userManager.Users.ToListAsync();

            // Filtriranje korisnika koji nisu admin
            var nonAdminUsers = new List<IdentityUser>();

            foreach (var user in allUsers)
            {
                if (!(await userManager.IsInRoleAsync(user, "Admin")))
                {
                    nonAdminUsers.Add(user);
                }
            }

            // Kreiranje rezultata koji uključuje korisničko ime, email, broj objekata i uloge
            var userDetails = new List<object>();

            foreach (var user in nonAdminUsers)
            {
                var roles = await userManager.GetRolesAsync(user);
                userDetails.Add(new
                {
                    user.UserName,
                    user.Email,
                    NumberOfObjects = dbContext.Places.Count(p => p.UserCreatedID == new Guid(user.Id)),
                    Roles = roles, // Vraća uloge korisnika
                    user.Id
                });
            }

            return Ok(userDetails);
        }


        [HttpDelete]
        [Route("delete/{userId}")]  
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            // Prvo pronađite korisnika
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Korisnik nije pronađen");
            }

            // Obrišite korisnika
            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Došlo je do greške prilikom brisanja korisnika");
            }
            // Povežite se sa drugom bazom podataka
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    // Obrišite mesta koja je korisnik kreirao
                    var places = dbContext.Places.Where(p => p.UserCreatedID == Guid.Parse(userId));
                    dbContext.Places.RemoveRange(places);

                    // Obrišite recenzije koje je korisnik ostavio
                    var reviews = dbContext.Reviews.Where(r => r.UserID == Guid.Parse(userId));
                    dbContext.Reviews.RemoveRange(reviews);

                    // Obrišite rezervacije korisnika
                    var reservations = dbContext.Reservations.Where(r => r.UserId == Guid.Parse(userId));
                    dbContext.Reservations.RemoveRange(reservations);

                    // Sačuvajte promene u bazi
                    await dbContext.SaveChangesAsync();

                    // Potvrdite transakciju
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // U slučaju greške, poništite transakciju
                    await transaction.RollbackAsync();
                    return StatusCode(500, $"Došlo je do greške: {ex.Message}");
                }
            }

            return Ok("Korisnik uspešno obrisan i povezani podaci su obrisani");
        }

        [HttpPut]
        [Route("changeRole/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(string userId, [FromBody] RoleChangeRequestDto request)
        {
            // Prvo pronađite korisnika
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("Korisnik nije pronađen");
            }

            // Uklonite prethodnu ulogu (ako postoji)
            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);

            // Dodajte novu ulogu
            var result = await userManager.AddToRoleAsync(user, request.Role);
            if (!result.Succeeded)
            {
                return BadRequest("Došlo je do greške prilikom promene uloge");
            }

            return Ok("Uloga uspešno promenjena");
        }

    }
}
