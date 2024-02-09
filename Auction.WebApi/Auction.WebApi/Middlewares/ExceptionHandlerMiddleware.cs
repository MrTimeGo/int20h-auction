
using Auction.WebApi.Expections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auction.WebApi.Middlewares;

public class ExceptionHandlerFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is HttpException httpException)
        {
            context.Result = new ObjectResult(new ErrorResult() { 
                Title = httpException.StatusCode.ToString(),
                Error = httpException.Message,
                Status = (int)httpException.StatusCode
            })
            {
                StatusCode = (int?)httpException.StatusCode
            };

            context.ExceptionHandled = true;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}

class ErrorResult
{
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Error { get; set; } = string.Empty;
}
