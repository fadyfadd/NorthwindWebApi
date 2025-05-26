namespace NorthwindWebApi.DataTransferObject;

public class ErrorDto
{
    public string ErrorMessage { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
    
    public string ErrorType { set; get; }
}