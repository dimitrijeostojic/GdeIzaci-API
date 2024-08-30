using GdeIzaci.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly GdeIzaciDBContext dbContext;

        public UserController(UserManager<IdentityUser> userManager, GdeIzaciDBContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
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
    }
}
