using GdeIzaci.Data;
using GdeIzaci.Models.DTO;
using GdeIzaci.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly GdeIzaciDBContext dbContext;

        public UserService(UserManager<IdentityUser> userManager, GdeIzaciDBContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, RoleChangeRequestDto request)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, currentRoles);

            var result = await userManager.AddToRoleAsync(user, request.Role);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return false;
            }

            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var places = dbContext.Places.Where(p => p.UserCreatedID == Guid.Parse(userId));
                    dbContext.Places.RemoveRange(places);

                    var reviews = dbContext.Reviews.Where(r => r.UserID == Guid.Parse(userId));
                    dbContext.Reviews.RemoveRange(reviews);

                    var reservations = dbContext.Reservations.Where(r => r.UserId == Guid.Parse(userId));
                    dbContext.Reservations.RemoveRange(reservations);

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }

            return true;
        }

        public async Task<IEnumerable<object>> GetAllNonAdminUsersAsync()
        {
            var allUsers = await userManager.Users.ToListAsync();
            var nonAdminUsers = new List<IdentityUser>();

            foreach (var user in allUsers)
            {
                if (!(await userManager.IsInRoleAsync(user, "Admin")))
                {
                    nonAdminUsers.Add(user);
                }
            }

            var userDetails = new List<object>();

            foreach (var user in nonAdminUsers)
            {
                var roles = await userManager.GetRolesAsync(user);
                userDetails.Add(new
                {
                    user.UserName,
                    user.Email,
                    NumberOfObjects = dbContext.Places.Count(p => p.UserCreatedID == new Guid(user.Id)),
                    Roles = roles,
                    user.Id
                });
            }

            return userDetails;
        }

        public async Task<IEnumerable<object>> GetManagersAsync()
        {
            var usersInManagerRole = await userManager.GetUsersInRoleAsync("Manager");
            var managers = new List<object>();

            foreach (var user in usersInManagerRole)
            {
                var roles = await userManager.GetRolesAsync(user);
                managers.Add(new
                {
                    user.UserName,
                    user.Email,
                    NumberOfObjects = dbContext.Places.Count(p => p.UserCreatedID == new Guid(user.Id)),
                    Roles = roles,
                    user.Id
                });
            }

            return managers;
        }

        public async Task<int> GetObjectCount(Guid id)
        {
            return await dbContext.Places.CountAsync(p => p.UserCreatedID == id);
        }

        public async Task<IdentityUser> GetUserByIdAsync(Guid id)
        {
            return await userManager.FindByIdAsync(id.ToString());
        }
    }
}
