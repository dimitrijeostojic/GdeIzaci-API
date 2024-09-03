using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
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
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
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

            var identityResult = await authService.RegisterUserAsync(registerRequestDto);

            if (!identityResult.Succeeded)
            {
                var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
                return BadRequest($"Error creating user: {errors}");
            }

            var user = await authService.FindUserByUsernameAsync(registerRequestDto.Username);
            var jwtToken = await authService.GenerateJwtTokenAsync(user);

            return Ok(new
            {
                Message = "User was registered successfully, please Login",
                JwtToken = jwtToken,
                Username = registerRequestDto.Username,
                Roles = await authService.GetUserRolesAsync(user)
            });
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

            var user = await authService.FindUserByUsernameAsync(loginRequestDto.Username);

            if (user != null && await authService.CheckPasswordAsync(user, loginRequestDto.Password))
            {
                var jwtToken = await authService.GenerateJwtTokenAsync(user);

                return Ok(new
                {
                    JwtToken = jwtToken,
                    Username = loginRequestDto.Username,
                    Roles = await authService.GetUserRolesAsync(user)
                });
            }

            return BadRequest("Wrong credentials!");
        }
    }
}
