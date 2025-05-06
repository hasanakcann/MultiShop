using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiShop.IdentityServer.Tools;

public static class JwtTokenGenerator
{
    public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model), "User model cannot be null.");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, model.Id)
        };

        if (!string.IsNullOrWhiteSpace(model.Role))
            claims.Add(new Claim(ClaimTypes.Role, model.Role));

        if (!string.IsNullOrWhiteSpace(model.UserName))
            claims.Add(new Claim("UserName", model.UserName));

        var keyBytes = Encoding.UTF8.GetBytes(JwtTokenDefaults.Key);
        if (keyBytes.Length < 32)
            throw new SecurityTokenException("The signing key must be at least 256 bits (32 bytes) long.");

        var key = new SymmetricSecurityKey(keyBytes);
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

        var token = new JwtSecurityToken(
            issuer: JwtTokenDefaults.ValidIssuer,
            audience: JwtTokenDefaults.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expireDate,
            signingCredentials: signingCredentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.WriteToken(token);

        return new TokenResponseViewModel(jwt, expireDate);
    }
}
