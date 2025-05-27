using System.Security.Claims;
using NorthwindWebApi.DataTransferObject;


namespace NorthwindWebApi.Security;

public interface IJwtService
{
    Task<UserProfileDto>   CreateJwtToken(ApplicationUser user , String userRole);
    Task<ClaimsPrincipal>  GetPrincipalFromJwtToken(string? token );
}