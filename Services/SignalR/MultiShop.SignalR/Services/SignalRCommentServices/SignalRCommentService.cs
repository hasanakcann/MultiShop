namespace MultiShop.SignalR.Services.SignalRCommentServices;

public class SignalRCommentService : ISignalRCommentService
{
    private readonly HttpClient _httpClient;
    public SignalRCommentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetTotalCommentCountAsync()
    {
        var responseMessage = await _httpClient.GetAsync("comments/gettotalcommentcount");
        var totalCommentCount = await responseMessage.Content.ReadFromJsonAsync<int>();
        return totalCommentCount;
    }
}
