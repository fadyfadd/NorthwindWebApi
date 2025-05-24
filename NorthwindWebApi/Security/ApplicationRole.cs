using Microsoft.AspNetCore.Identity;

namespace NorthwindWebApi.Security;

public class ApplicationRole: IdentityRole<Guid>
{
    public String FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
}