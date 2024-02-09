using Auction.WebApi.Dto.Identity;
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auction.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IdentityController(
    IAuthService authService
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {
       var result = await authService.RegisterUserAsync(dto);

        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokensDto>> Login([FromBody] LoginDto dto)
    {
        return await authService.LoginUserAsync(dto);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await authService.LogoutAsync(User);
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshToken(TokensDto tokens)
    {
        return Ok(new RefreshTokenDto { RefreshToken = await authService.GenerateRefreshTokenAsync(tokens) });
    }
}
