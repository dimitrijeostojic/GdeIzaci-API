using GdeIzaci.Models.DTO;
using GdeIzaci.Repository.Interfaces;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthService(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityUser> FindUserByUsernameAsync(string username)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var roles = await GetUserRolesAsync(user);
            return tokenRepository.CreateJWTToken(user, roles.ToList());
        }

        public async Task<IList<string>> GetUserRolesAsync(IdentityUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Email,
            };

            var result = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (result.Succeeded && registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                result = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
            }

            return result;
        }
    }
}
