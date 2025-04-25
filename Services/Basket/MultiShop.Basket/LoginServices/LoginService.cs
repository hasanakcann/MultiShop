namespace MultiShop.Basket.LoginServices;

public class LoginService : ILoginService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor), "HttpContextAccessor cannot be null.");
    }

    public string GetUserId
    {
        get
        {
            var userClaim = _httpContextAccessor.HttpContext?.User.FindFirst("sub");
            if (userClaim == null || string.IsNullOrWhiteSpace(userClaim.Value))
                throw new InvalidOperationException("User ID is not found in the claims.");

            return userClaim.Value;
        }
    }
}
