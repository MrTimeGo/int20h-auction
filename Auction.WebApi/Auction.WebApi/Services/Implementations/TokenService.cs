using Auction.WebApi.Entities;
using Auction.WebApi.Options;
using Auction.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auction.WebApi.Services.Implementations;

public class TokenService(IOptions<JwtOptions> options) : ITokenService
{
    public string GenerateAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(/* key */);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
            };
        var token = new JwtSecurityToken(/* issuer */,
            /* audience */,
            claims,
            expires: DateTime.Now.AddMinutes(/* exprires after */),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (string, DateTime) GenerateRefreshToken(User user)
    {
        throw new NotImplementedException();
    }
}
