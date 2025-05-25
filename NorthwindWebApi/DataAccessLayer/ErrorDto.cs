namespace NorthWindWebApi.DataAccessLayer;

public class ErrorDto
{
    public String ErrorMessage { get; set; }
    public Dictionary<String, String[]> Errors { get; set; }
    
    public String ErrorType { set; get; }
}