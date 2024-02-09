using Auction.WebApi.Dto.Identity;
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auction.WebApi.Services.Implementations
{
    public class AuthService(SignInManager<User> signInManager,
                            ITokenService tokenService,
                            IUserService userService,
                            ILogger<AuthService> logger) : IAuthService
    {
        public async Task<string> GenerateRefreshTokenAsync(TokensDto tokens)
        {
            var claimPrincipal = tokenService.GetClaimsPrincipalFromToken(tokens.AccessToken);
            var user = await userService.GetUserByClaimPrincipalsAsync(claimPrincipal);
            if (user is null || user.RefreshToken != tokens.RefreshToken || user.RefreshTokenExpiresAt <= DateTime.UtcNow)
            {
                logger.LogError("User was not found with provided access token");
                throw new Exception("User not found");
            }

            if (user.RefreshToken != tokens.RefreshToken)
            {
                logger.LogError("Invalid refresh token");
                throw new Exception("Invalid refresh token");
            }

            if (user.RefreshTokenExpiresAt <= DateTime.UtcNow)
            {
                logger.LogError("Refresh token expired");
                throw new Exception("Refresh token expired");
            }


            return tokenService.GenerateAccessToken(user);
        }

        public async Task<TokensDto> LoginUserAsync(LoginDto dto)
        {
            var user = await userService.GetUserByEmailOrUsernameAsync(dto.EmailOrUsername);
            if (user is null)
            {
                throw new Exception("Token expired");
            }
            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if (!result.Succeeded)
            {
                throw new Exception("Token expired");
            }

            var accessToken = tokenService.GenerateAccessToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken.Item1;
            user.RefreshTokenExpiresAt = refreshToken.Item2;
            var updateReult = await userService.UpdateUserAsync(user);

            if (!updateReult.Succeeded)
            {
                throw new Exception("Token expired");
            }

            return new TokensDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Item1
            };
        }

        public async Task LogoutAsync(ClaimsPrincipal principal)
        {
            var user = await userService.GetUserByClaimPrincipalsAsync(principal);
            if (user is null)
            {
                throw new Exception("Token expired");
            }
            user.RefreshToken = string.Empty;
            user.RefreshTokenExpiresAt = DateTime.UtcNow;
            await userService.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> RegisterUserAsync(CreateUserDto dto)
        {
            var user = await userService.GetUserByEmailAsync(dto.Email);
            if (user is not null)
            {
                throw new Exception("Token expired");
            }
            return await userService.CreateUserAsync(new User()
            {
                Email = dto.Email,
                UserName = dto.Username
            }, dto.Password);
        }
    }
}
