namespace WebApiNorthwind.DataTransferObject;

public class LoginDto : BaseDto
{
    public String UserName { get; set; }
    public String Password { get; set; }
}