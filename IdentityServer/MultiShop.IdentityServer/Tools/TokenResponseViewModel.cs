using System;

namespace MultiShop.IdentityServer.Tools;

public class TokenResponseViewModel
{
    public TokenResponseViewModel(string token, DateTime expireDate)
    {
        Token = string.IsNullOrWhiteSpace(token)
            ? throw new ArgumentException("Token cannot be null or empty.", nameof(token))
            : token;

        ExpireDate = expireDate;
    }

    public string Token { get; }
    public DateTime ExpireDate { get; }
}
