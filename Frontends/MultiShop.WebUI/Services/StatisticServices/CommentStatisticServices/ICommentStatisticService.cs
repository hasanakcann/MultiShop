namespace MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;

public interface ICommentStatisticService
{
    Task<int> GetTotalCommentCountAsync();
    Task<int> GetActiveCommentCountAsync();
    Task<int> GetPassiveCommentCountAsync();
}
