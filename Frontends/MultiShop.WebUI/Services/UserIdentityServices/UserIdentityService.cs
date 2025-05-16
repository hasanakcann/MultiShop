using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.UserIdentityServices;

public class UserIdentityService : IUserIdentityService
{
    private readonly HttpClient _httpClient;
    private static readonly Uri getAllUsersEndpoint = new("http://localhost:5001/api/users/getalluserlist");

    public UserIdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultUserDto>> GetAllUserListAsync()
    {
        var response = await _httpClient.GetAsync(getAllUsersEndpoint);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrWhiteSpace(content))
            throw new InvalidOperationException("Received empty user data from the server.");

        var result = JsonConvert.DeserializeObject<UserListResponseDto>(content)
                     ?? throw new InvalidOperationException("Failed to deserialize user data.");

        return result.Data;
    }

}
