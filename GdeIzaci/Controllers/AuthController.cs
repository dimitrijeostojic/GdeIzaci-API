using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Kreiranje IdentityUser objekta sa podacima iz zahteva
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,
            };

            // Kreiranje korisnika u sistemu
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (!identityResult.Succeeded)
            {
                var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                return BadRequest($"Error creating user: {errors}");
            }

            // Dodavanje uloga korisniku ako je registracija uspešna
            if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (!identityResult.Succeeded)
                {
                    var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                    return BadRequest($"Error adding roles: {errors}");
                }
            }

            //Generisanje tokena
            var roles = await userManager.GetRolesAsync(identityUser);
            if (roles != null)
            {
                //Kreiranje tokena
                var jwtToken = tokenRepository.CreateJWTToken(identityUser, roles.ToList());
                return Ok(new
                {
                    Message = "User was registered successfully, please Login",
                    JwtToken = jwtToken,
                    Username = registerRequestDto.Username,
                    Roles = roles
                });
            }

            return Ok("User was registered, please Login");
        }


        // POST: localhost:8071/api/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Pronalaženje korisnika po email adresi
            var user = await userManager.FindByNameAsync(loginRequestDto.Username);
            if (user != null)
            {
                // Provera lozinke
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    //Get roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Kreiranje tokena
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        return Ok(new
                        {
                            JwtToken = jwtToken,
                            Username = loginRequestDto.Username,
                            Roles = roles
                        });
                    }
                }
            }
            return BadRequest("Wrong credentials!");
        }
    }
}
