using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices;

public class MessageService : IMessageService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "http://localhost:5000/services/message/usermessages/";

    public MessageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
    {
        var responseMessage = await _httpClient.GetAsync($"{BaseUrl}getmessageinbox?id={id}");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ApplicationException("Failed to fetch inbox messages from the API.");
        }

        var inboxMessages = await responseMessage.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>();
        return inboxMessages ?? new List<ResultInboxMessageDto>();
    }

    public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
    {
        var responseMessage = await _httpClient.GetAsync($"{BaseUrl}getmessagesendbox?id={id}");

        if (!responseMessage.IsSuccessStatusCode)
        {
            throw new ApplicationException("Failed to fetch sent messages from the API.");
        }

        var sendboxMessages = await responseMessage.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>();
        return sendboxMessages ?? new List<ResultSendboxMessageDto>();
    }
}
