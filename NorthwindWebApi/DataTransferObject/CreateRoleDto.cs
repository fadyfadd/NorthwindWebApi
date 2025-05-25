using NorthwindWebApi.Security;

namespace WebApiNorthwind.DataTransferObject;

public class CreateRoleDto : BaseDto
{
    public String Role { get; set; }
}