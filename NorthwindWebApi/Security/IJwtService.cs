using System.Security.Claims;
using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.Security;

public interface IJwtService
{
    Task<UserProfileDto>   CreateJwtToken(ApplicationUser user , String userRole);
    Task<ClaimsPrincipal>  GetPrincipalFromJwtToken(string? token );
}