using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.Interfaces;
using System.Text.Json;

namespace MultiShop.WebUI.Services.Concrete;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<UserDetailViewModel> GetUserInfo()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/users/getuser");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve user info. Status Code: {response.StatusCode}");
            }

            var user = await response.Content.ReadFromJsonAsync<UserDetailViewModel>();

            if (user == null)
            {
                throw new InvalidOperationException("User data returned null.");
            }

            return user;
        }
        catch (HttpRequestException ex)
        {
            throw new ApplicationException("An error occurred while calling the user service.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new ApplicationException("The content type is not supported.", ex);
        }
        catch (JsonException ex)
        {
            throw new ApplicationException("Invalid JSON returned from the user service.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while fetching user information.", ex);
        }
    }
}
