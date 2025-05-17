namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;

public class CommentStatisticService : ICommentStatisticService
{
    private readonly HttpClient _httpClient;
    public CommentStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetActiveCommentCount()
    {
        var responseMessage = await _httpClient.GetAsync("comments/getactivecommentcount");
        var activeCommentCount = await responseMessage.Content.ReadFromJsonAsync<int>();
        return activeCommentCount;
    }

    public async Task<int> GetPassiveCommentCount()
    {
        var responseMessage = await _httpClient.GetAsync("comments/getpassivecommentcount");
        var passiveCommentCount = await responseMessage.Content.ReadFromJsonAsync<int>();
        return passiveCommentCount;
    }

    public async Task<int> GetTotalCommentCount()
    {
        var responseMessage = await _httpClient.GetAsync("comments/gettotalcommentcount");
        var totalCommentCount = await responseMessage.Content.ReadFromJsonAsync<int>();
        return totalCommentCount;
    }
}
