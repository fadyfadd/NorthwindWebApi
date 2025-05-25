namespace WebApiNorthwind.DataTransferObject;

public class CreateUserDto : BaseDto
{
    public String FullName { set; get; }
    public String UserName { get; set; }
    public String Password { get; set; }
    public String Email { set; get; }
    public DateTime BirthDate { set; get; }
}