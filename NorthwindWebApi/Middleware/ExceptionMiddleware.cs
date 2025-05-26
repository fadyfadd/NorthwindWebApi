using System.Net;
using System.Text.Json;
using NorthwindWebApi.Exceptions;
using NorthwindWebApi.DataTransferObject;

namespace NorthwindWebApi.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (ex is NorthwindWebApiException businessException)
            {
                if (businessException.InnerException is not null)
                    _logger.LogError(businessException.InnerException.ToString());

                ErrorDto error = new ErrorDto()
                {
                    ErrorMessage = ex.Message,
                    Errors = businessException.Errors,
                    ErrorType = businessException.ErrorType,
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(error, options));
            }
            else
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("");
            }
        }
    }
}