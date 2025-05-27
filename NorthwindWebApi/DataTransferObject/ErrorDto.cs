using WebApiNorthwind.DataTransferObject;

namespace NorthwindWebApi.DataTransferObject;

public class ErrorDto : BaseDto
{
    public string ErrorMessage { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
    
    public string ErrorType { set; get; }
}