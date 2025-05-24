using System.Security.Claims;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Security;

public class JwtService : IJwtService
{
    public UserSecurityProfileDto CreateJwtToken(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
    {
        throw new NotImplementedException();
    }
}