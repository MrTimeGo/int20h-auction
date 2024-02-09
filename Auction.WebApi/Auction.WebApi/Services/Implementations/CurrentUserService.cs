using Auction.WebApi.Services.Interfaces;

namespace Auction.WebApi.Services.Implementations;

public class CurrentUserService : ICurrentUserService
{
    public Guid? CurrentUserId { get; set; }
}
