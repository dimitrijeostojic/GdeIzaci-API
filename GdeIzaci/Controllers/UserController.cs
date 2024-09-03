using GdeIzaci.Data;
using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
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
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin, Manager, RegularUser")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var objectCount = await userService.GetObjectCount(id); // Izmenjeno da se poziva iz servisa
           

            return Ok(new
            {
                user.UserName,
                numberOfObjects = objectCount,
                userID = user.Id
            });
        }

        [HttpGet]
        [Route("managers")]
        [Authorize(Roles = "Manager, RegularUser")]
        public async Task<IActionResult> GetManagers()
        {
            var managers = await userService.GetManagersAsync();
            return Ok(managers);
        }

        [HttpGet]
        [Route("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllNonAdminUsers()
        {
            var users = await userService.GetAllNonAdminUsersAsync();
            return Ok(users);
        }


        [HttpDelete]
        [Route("delete/{userId}")]  
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await userService.DeleteUserAsync(userId);
            if (!result)
            {
                return BadRequest("Došlo je do greške prilikom brisanja korisnika");
            }

            return Ok("Korisnik uspešno obrisan i povezani podaci su obrisani");
        }

        [HttpPut]
        [Route("changeRole/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole(string userId, [FromBody] RoleChangeRequestDto request)
        {
            var result = await userService.ChangeUserRoleAsync(userId, request);
            if (!result)
            {
                return BadRequest("Došlo je do greške prilikom promene uloge");
            }

            return Ok("Uloga uspešno promenjena");
        }

    }
}
