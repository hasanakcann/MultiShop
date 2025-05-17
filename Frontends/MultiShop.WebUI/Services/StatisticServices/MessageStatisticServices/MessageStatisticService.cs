namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;

public class MessageStatisticService : IMessageStatisticService
{
    private readonly HttpClient _httpClient;
    public MessageStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetTotalMessageCountAsync()
    {
        var responseMessage = await _httpClient.GetAsync("usermessages/gettotalmessagecount");
        var totalMessageCount = await responseMessage.Content.ReadFromJsonAsync<int>();
        return totalMessageCount;
    }
}
