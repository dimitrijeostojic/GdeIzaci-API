using GdeIzaci.Models.DTO;
using GdeIzaci.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GdeIzaci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //localhost:8051/api/register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles to this User
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("Something went wrong!");
        }

        // POST: localhost:8071/api/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByNameAsync(loginRequestDto.Username);
            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get roles for this user

                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token

                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }


                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
