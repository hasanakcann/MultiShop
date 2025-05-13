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
        await ExecuteSafeAsync(async () =>
        {
            var message = _mapper.Map<UserMessage>(createMessageDto);
            await _messageContext.UserMessages.AddAsync(message);
            await _messageContext.SaveChangesAsync();
        }, "An error occurred while saving the message.");
    }

    public async Task DeleteMessageAsync(int id)
    {
        await ExecuteSafeAsync(async () =>
        {
            var message = await _messageContext.UserMessages.FindAsync(id);
            if (message == null)
                throw new KeyNotFoundException("The message to be deleted was not found.");

            _messageContext.UserMessages.Remove(message);
            await _messageContext.SaveChangesAsync();
        }, "An error occurred while deleting the message.");
    }

    public async Task<List<ResultMessageDto>> GetAllMessageAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var messages = await _messageContext.UserMessages.ToListAsync();
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }, "An error occurred while retrieving all messages.");
    }

    public async Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var message = await _messageContext.UserMessages.FindAsync(id);
            if (message == null)
                throw new KeyNotFoundException("The requested message was not found.");

            return _mapper.Map<GetByIdMessageDto>(message);
        }, "An error occurred while retrieving the message.");
    }

    public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var messages = await _messageContext.UserMessages
                .Where(x => x.ReceiverId == id)
                .ToListAsync();

            return _mapper.Map<List<ResultInboxMessageDto>>(messages);
        }, "An error occurred while retrieving inbox messages.");
    }

    public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var messages = await _messageContext.UserMessages
                .Where(x => x.SenderId == id)
                .ToListAsync();

            return _mapper.Map<List<ResultSendboxMessageDto>>(messages);
        }, "An error occurred while retrieving sent messages.");
    }

    public async Task UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        await ExecuteSafeAsync(async () =>
        {
            var message = _mapper.Map<UserMessage>(updateMessageDto);
            _messageContext.UserMessages.Update(message);
            await _messageContext.SaveChangesAsync();
        }, "An error occurred while updating the message.");
    }

    public async Task<int> GetTotalMessageCountAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            return await _messageContext.UserMessages.CountAsync();
        }, "An error occurred while retrieving the total message count.");
    }

    private async Task ExecuteSafeAsync(Func<Task> action, string errorMessage)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(errorMessage, ex);
        }
    }

    private async Task<T> ExecuteSafeAsync<T>(Func<Task<T>> func, string errorMessage)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(errorMessage, ex);
        }
    }
}
