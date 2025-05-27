namespace NorthwindWebApi.Exceptions;

public class NorthwindWebApiException : Exception
{
    
    public Dictionary<string, string[]> Errors { get; set; }
    public String ErrorType { set; get; }
 
    public NorthwindWebApiException(String message ,  String errorType , Dictionary<string, string[]> errors = null ,  Exception innerException = null) : base(message , innerException)
    {
        this.Errors = errors;
        this.ErrorType = errorType;
 
    }
}