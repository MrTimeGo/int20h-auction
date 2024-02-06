using Auction.WebApi.Entities;

namespace Auction.WebApi.Services.Interfaces;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    public (string, DateTime) GenerateRefreshToken(User user);
}
