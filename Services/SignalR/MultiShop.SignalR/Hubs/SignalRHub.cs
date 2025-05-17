using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalR.Services.SignalRCommentServices;

namespace MultiShop.SignalR.Hubs;

public class SignalRHub : Hub
{
    private readonly ISignalRCommentService _signalRCommentService;
    public SignalRHub(ISignalRCommentService signalRCommentService)
    {
        _signalRCommentService = signalRCommentService;
    }

    public async Task SendStatisticCount()
    {
        var totalCommentCount = await _signalRCommentService.GetTotalCommentCountAsync();
        await Clients.All.SendAsync("ReceiveTotalCommentCount", totalCommentCount);
    }
}
