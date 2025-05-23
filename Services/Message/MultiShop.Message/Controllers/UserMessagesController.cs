﻿using Microsoft.AspNetCore.Authorization;
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
        var allMessages = await _userMessageService.GetAllMessageAsync();
        return Ok(allMessages);
    }

    [HttpGet("GetMessageSendbox")]
    public async Task<IActionResult> GetMessageSendbox(string id)
    {
        var sendboxMessages = await _userMessageService.GetSendboxMessageAsync(id);
        return Ok(sendboxMessages);
    }

    [HttpGet("GetMessageInbox")]
    public async Task<IActionResult> GetMessageInbox(string id)
    {
        var inboxMessages = await _userMessageService.GetInboxMessageAsync(id);
        return Ok(inboxMessages);
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
        int totalMessageCount = await _userMessageService.GetTotalMessageCountAsync();
        return Ok(totalMessageCount);
    }

    [HttpGet("GetTotalMessageCountByReceiverId")]
    public async Task<IActionResult> GetTotalMessageCountByReceiverId(string id)
    {
        int totalMessageCountByReceiver = await _userMessageService.GetTotalMessageCountByReceiverIdAsync(id);
        return Ok(totalMessageCountByReceiver);
    }
}
