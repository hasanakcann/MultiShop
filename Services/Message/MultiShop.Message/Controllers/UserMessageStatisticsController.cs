﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class UserMessageStatisticsController : ControllerBase
{
    private readonly IUserMessageService _userMessageService;
    public UserMessageStatisticsController(IUserMessageService userMessageService)
    {
        _userMessageService = userMessageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTotalMessageCount()
    {
        int messageCount = await _userMessageService.GetTotalMessageCountAsync();
        return Ok(messageCount);
    }
}
