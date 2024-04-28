using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GdeIzaci.Data
{
    public class GdeIzaciAuthDBContext : IdentityDbContext
    {
        public GdeIzaciAuthDBContext(DbContextOptions<GdeIzaciAuthDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var regularUserRoleId = "b628e50d-b782-4304-a229-bcc6527f4f61";
            var managerRoleId = "a331f43f-eb04-4eec-8670-df7a59490434";
            var adminRoleId = "218f2cea-436f-45b4-a360-a99f82618518";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id=regularUserRoleId,
                    ConcurrencyStamp=regularUserRoleId,
                    Name="RegularUser",
                    NormalizedName="RegularUser".ToUpper()
                },new IdentityRole()
                {
                    Id=managerRoleId,
                    ConcurrencyStamp=managerRoleId,
                    Name="Manager",
                    NormalizedName="Manager".ToUpper()
                },new IdentityRole()
                {
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId,
                    Name="Admin",
                    NormalizedName="Admin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
