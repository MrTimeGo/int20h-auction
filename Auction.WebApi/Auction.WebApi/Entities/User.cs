﻿using Microsoft.AspNetCore.Identity;

namespace Auction.WebApi.Entities;

public class User : IdentityUser<Guid>
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiresAt { get; set; }
}
