using Auction.WebApi.Dto.Identity;
using Auction.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auction.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IdentityController(
    UserManager<User> userManager,
    SignInManager<User> signInManager
    ) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {
        var result = await userManager.CreateAsync(new User()
        {
            Email = dto.Email,
            UserName = dto.Username
        }, dto.Password);


        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPost("/login")]
    public async Task<ActionResult<TokensDto>> Login([FromBody] LoginDto dto)
    {
        var user = await userManager.FindByNameAsync(dto.EmailOrUsername) ?? await userManager.FindByEmailAsync(dto.EmailOrUsername);
        if (user is null)
        {
            return Unauthorized();
        }
        var result = await signInManager.PasswordSignInAsync(user, dto.Password, false, false);

        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        ////// 1. Generate jwt access token
        // 2. Generate and save refresh token
        var accessToken = "adasda"; // TODO
        var refreshToken = "adsadas"; // TODO
        /////


        return new TokensDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
