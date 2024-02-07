
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Auction.WebApi.Middlewares;

public class CurrentUserMiddleware(ICurrentUserService currentUserService) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.User is null)
        {
            await next(context);
            return;
        }

        //var user = await userManager.GetUserAsync(context.User);

        //currentUserService.CurrentUserId = user!.Id;
        await next(context);
    }
}
