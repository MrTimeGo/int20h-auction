using Auction.WebApi.Entities;
using System.Security.Claims;

namespace Auction.WebApi.Services.Interfaces;

public interface ITokenService
{
    public string GenerateAccessToken(User user);
    public (string, DateTime) GenerateRefreshToken();
    public ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
}
