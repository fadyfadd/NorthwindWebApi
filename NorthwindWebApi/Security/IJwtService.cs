using System.Security.Claims;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Security;

public interface IJwtService
{
    UserProfileDto CreateJwtToken(ApplicationUser user , Role userRole);
    ClaimsPrincipal? GetPrincipalFromJwtToken(string? token );
}