namespace Auction.WebApi.Services.Interfaces;

public interface ICurrentUserService
{
    Guid? CurrentUserId { get; set; }
}
