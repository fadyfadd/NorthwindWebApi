using NorthwindWebApi.Security;

namespace WebApiNorthwind.DataTransferObject;

public class UserProfileDto : BaseDto
{
    public string? FullName { get; set; } = string.Empty;
    public String? UserName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
    public DateTime TokenExpiration { get; set; }
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime? RefreshTokenExpirationDateTime { get; set; }
    public Role UserRole { get; set; }
}
 