using System.Security.Claims;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Security;

public interface IJwtService
{
    UserSecurityProfileDto CreateJwtToken(ApplicationUser user);
    ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
}