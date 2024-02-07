
using Auction.WebApi.Services.Interfaces;
using System.Security.Claims;


namespace Auction.WebApi.Middlewares;

public class CurrentUserMiddleware(ICurrentUserService currentUserService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User is null || context.User.Identity is null || !context.User.Identity.IsAuthenticated)
        {
            await next(context);
            return;
        }

        currentUserService.CurrentUserId = new Guid(context.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        
        await next(context);
    }
}
