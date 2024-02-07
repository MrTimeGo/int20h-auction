using Auction.WebApi.Dto.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auction.WebApi.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<IdentityResult> RegisterUserAsync(CreateUserDto dto);
        public Task<TokensDto> LoginUserAsync(LoginDto dto);
        public Task LogoutAsync(ClaimsPrincipal principal);
        public Task<string> GenerateRefreshTokenAsync(TokensDto tokens);
    }
}
