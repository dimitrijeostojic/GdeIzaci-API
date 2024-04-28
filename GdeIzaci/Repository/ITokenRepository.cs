using Microsoft.AspNetCore.Identity;

namespace GdeIzaci.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
