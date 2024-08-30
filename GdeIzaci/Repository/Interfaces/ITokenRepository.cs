using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Repository.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
