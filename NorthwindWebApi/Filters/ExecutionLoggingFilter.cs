using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NorthwindWebApi.Filters;

public class ExecutionLoggingFilter : IActionFilter
{
    ILogger<ExecutionLoggingFilter> _logger;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var actionName = context.ActionDescriptor.DisplayName.ToString();
        _logger.LogInformation($"Begin Action Execution: {actionName} at {currentDate} ");
    }

    public ExecutionLoggingFilter(ILogger<ExecutionLoggingFilter> logger)
    {
        this._logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var actionName = context.ActionDescriptor.DisplayName.ToString();
        _logger.LogInformation($"End Action Execution : {actionName} at {currentDate} ");
    }
}