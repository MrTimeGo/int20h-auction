using Auction.WebApi.Dto.Identity;
using Auction.WebApi.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auction.WebApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<IdentityResult> CreateUserAsync(User user, string password);
        public Task<IdentityResult> UpdateUserAsync(User user);
        public Task<User?> GetUserByEmailOrUsernameAsync(string emailOrUsername);
        public Task<User?> GetUserByClaimPrincipalsAsync(ClaimsPrincipal principal);

    }
}
