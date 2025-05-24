using Microsoft.AspNetCore.Identity;

namespace NorthwindWebApi.Security;

public class ApplicationUser : IdentityUser<Guid>
{
    public String FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
}