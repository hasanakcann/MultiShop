using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.UserIdentityServices;

public class UserIdentityService : IUserIdentityService
{
    private readonly HttpClient _httpClient;
    public UserIdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultUserDto>> GetAllUserListAsync()
    {
        var responseMessage = await _httpClient.GetAsync("http://localhost:5001/api/users/getalluserlist");

        if (!responseMessage.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to retrieve user list. StatusCode: {responseMessage.StatusCode}");

        var jsonData = await responseMessage.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(jsonData))
            throw new Exception("Empty user data received from server.");

        var userInfo = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);

        if (userInfo == null)
            throw new Exception("Failed to deserialize user data.");

        return userInfo;
    }
}
