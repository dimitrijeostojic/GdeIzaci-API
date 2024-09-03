using GdeIzaci.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequestDto);
        Task<IdentityUser> FindUserByUsernameAsync(string username);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<string> GenerateJwtTokenAsync(IdentityUser user);
        Task<IList<string>> GetUserRolesAsync(IdentityUser user);
    }
}
