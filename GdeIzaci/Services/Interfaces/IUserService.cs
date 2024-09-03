using GdeIzaci.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityUser> GetUserByIdAsync(Guid id);
        Task<IEnumerable<object>> GetManagersAsync();
        Task<IEnumerable<object>> GetAllNonAdminUsersAsync();
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, RoleChangeRequestDto request);

        Task<int> GetObjectCount(Guid id);
    }
}
