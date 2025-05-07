using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CommentServices;

public class CommentService : ICommentService
{
    private readonly HttpClient _httpClient;

    public CommentService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
    {
        if (createCommentDto == null)
            throw new ArgumentNullException(nameof(createCommentDto));

        var response = await _httpClient.PostAsJsonAsync("comments", createCommentDto);
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException("Failed to create comment.");
    }

    public async Task DeleteCommentAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id));

        var response = await _httpClient.DeleteAsync($"comments?id={id}");
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Failed to delete comment with ID: {id}");
    }

    public async Task<List<ResultCommentDto>> GetAllCommentAsync()
    {
        var response = await _httpClient.GetAsync("comments");

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException("Failed to retrieve comments.");

        var jsonData = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
        return values ?? new List<ResultCommentDto>();
    }

    public async Task<UpdateCommentDto> GetByIdCommentAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id));

        var response = await _httpClient.GetAsync($"comments/{id}");
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Failed to get comment with ID: {id}");

        var comment = await response.Content.ReadFromJsonAsync<UpdateCommentDto>();
        return comment ?? throw new ApplicationException("Comment not found.");
    }

    public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
    {
        if (updateCommentDto == null)
            throw new ArgumentNullException(nameof(updateCommentDto));

        var response = await _httpClient.PutAsJsonAsync("comments", updateCommentDto);
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException("Failed to update comment.");
    }

    public async Task<List<ResultCommentDto>> CommentListByProductId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id));

        var response = await _httpClient.GetAsync($"comments/commentlistbyproductid/{id}");
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"Failed to get comments for product ID: {id}");

        var jsonData = await response.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
        return values ?? new List<ResultCommentDto>();
    }
}
