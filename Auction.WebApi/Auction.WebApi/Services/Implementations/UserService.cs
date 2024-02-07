using Auction.WebApi.Dto.Identity;
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auction.WebApi.Services.Implementations
{
    public class UserService(UserManager<User> userManager) : IUserService
    {
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
            return await userManager.CreateAsync(user, password);
        }

        public async Task<User?> GetUserByClaimPrincipalsAsync(ClaimsPrincipal principal)
        {
            return await userManager.GetUserAsync(principal);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<User?> GetUserByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await userManager.FindByNameAsync(emailOrUsername) ?? await userManager.FindByEmailAsync(emailOrUsername);
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await userManager.UpdateAsync(user);
        }
    }
}
