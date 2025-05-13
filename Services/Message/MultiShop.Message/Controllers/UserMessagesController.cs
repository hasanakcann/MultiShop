using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Dtos;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserMessagesController : ControllerBase
{
    private readonly IUserMessageService _userMessageService;
    public UserMessagesController(IUserMessageService userMessageService)
    {
        _userMessageService = userMessageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMessage()
    {
        var values = await _userMessageService.GetAllMessageAsync();
        return Ok(values);
    }

    [HttpGet("GetMessageSendbox")]
    public async Task<IActionResult> GetMessageSendbox(string id)
    {
        var values = await _userMessageService.GetSendboxMessageAsync(id);
        return Ok(values);
    }

    [HttpGet("GetMessageInbox")]
    public async Task<IActionResult> GetMessageInbox(string id)
    {
        var values = await _userMessageService.GetInboxMessageAsync(id);
        return Ok(values);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessageAsync(CreateMessageDto createMessageDto)
    {
        await _userMessageService.CreateMessageAsync(createMessageDto);
        return Ok("Message successfully added.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMessageAsync(int id)
    {
        await _userMessageService.DeleteMessageAsync(id);
        return Ok("Message successfully deleted.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        await _userMessageService.UpdateMessageAsync(updateMessageDto);
        return Ok("Message successfully updated.");
    }

    [HttpGet("GetTotalMessageCount")]
    public async Task<IActionResult> GetTotalMessageCount()
    {
        int values = await _userMessageService.GetTotalMessageCountAsync();
        return Ok(values);
    }
}
