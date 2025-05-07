using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.DAL.Entities;
using MultiShop.Message.Dtos;

namespace MultiShop.Message.Services;

public class UserMessageService : IUserMessageService
{
    private readonly MessageContext _messageContext;
    private readonly IMapper _mapper;
    public UserMessageService(MessageContext messageContext, IMapper mapper)
    {
        _messageContext = messageContext;
        _mapper = mapper;
    }

    public async Task CreateMessageAsync(CreateMessageDto createMessageDto)
    {
        var message = _mapper.Map<UserMessage>(createMessageDto);
        await _messageContext.UserMessages.AddAsync(message);
        await _messageContext.SaveChangesAsync();
    }

    public async Task DeleteMessageAsync(int id)
    {
        var message = await _messageContext.UserMessages.FindAsync(id);
        _messageContext.UserMessages.Remove(message);
        await _messageContext.SaveChangesAsync();
    }

    public async Task<List<ResultMessageDto>> GetAllMessageAsync()
    {
        var messages = await _messageContext.UserMessages.ToListAsync();
        return _mapper.Map<List<ResultMessageDto>>(messages);
    }

    public async Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
    {
        var message = await _messageContext.UserMessages.FindAsync(id);
        return _mapper.Map<GetByIdMessageDto>(message);
    }

    public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
    {
        var inboxMessages = await _messageContext.UserMessages.Where(x => x.ReceiverId == id).ToListAsync();
        return _mapper.Map<List<ResultInboxMessageDto>>(inboxMessages);
    }

    public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
    {
        var sendboxMessages = await _messageContext.UserMessages.Where(x => x.SenderId == id).ToListAsync();
        return _mapper.Map<List<ResultSendboxMessageDto>>(sendboxMessages);
    }

    public async Task UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        var message = _mapper.Map<UserMessage>(updateMessageDto);
        _messageContext.UserMessages.Update(message);
        await _messageContext.SaveChangesAsync();
    }

    public async Task<int> GetTotalMessageCount()
    {
        int totalMessageCount = await _messageContext.UserMessages.CountAsync();
        return totalMessageCount;
    }
}
