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
        return BadRequest();
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokensDto>> Login([FromBody] LoginDto dto)
    {
        try
        {
            return await authService.LoginUserAsync(dto);
        }
        catch
        {
            return Unauthorized();
        }
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            await authService.LogoutAsync(User);
            return Ok();
        }
        catch 
        {
            return Unauthorized();
        }
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<string>> RefreshToken(TokensDto tokens)
    {
        try
        {
            return await authService.GenerateRefreshTokenAsync(tokens);
        }
        catch
        {
            return Unauthorized();
        }
    }
}
