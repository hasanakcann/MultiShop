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
        try
        {
            var message = _mapper.Map<UserMessage>(createMessageDto);
            await _messageContext.UserMessages.AddAsync(message);
            await _messageContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("An error occurred while saving the message. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while creating the message.", ex);
        }
    }

    public async Task DeleteMessageAsync(int id)
    {
        try
        {
            var message = await _messageContext.UserMessages.FindAsync(id);

            if (message == null)
                throw new KeyNotFoundException("The message you are trying to delete was not found.");

            _messageContext.UserMessages.Remove(message);
            await _messageContext.SaveChangesAsync();
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("An error occurred while deleting the message. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while deleting the message.", ex);
        }
    }

    public async Task<List<ResultMessageDto>> GetAllMessageAsync()
    {
        try
        {
            var messages = await _messageContext.UserMessages.ToListAsync();
            return _mapper.Map<List<ResultMessageDto>>(messages);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all messages. Please try again later.", ex);
        }
    }

    public async Task<GetByIdMessageDto> GetByIdMessageAsync(int id)
    {
        try
        {
            var message = await _messageContext.UserMessages.FindAsync(id);

            if (message == null)
                throw new KeyNotFoundException("The message you are looking for was not found.");

            return _mapper.Map<GetByIdMessageDto>(message);
        }
        catch (KeyNotFoundException ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while retrieving the message.", ex);
        }
    }

    public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
    {
        try
        {
            var inboxMessages = await _messageContext.UserMessages.Where(x => x.ReceiverId == id).ToListAsync();
            return _mapper.Map<List<ResultInboxMessageDto>>(inboxMessages);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving inbox messages. Please try again later.", ex);
        }
    }

    public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
    {
        try
        {
            var sendboxMessages = await _messageContext.UserMessages.Where(x => x.SenderId == id).ToListAsync();
            return _mapper.Map<List<ResultSendboxMessageDto>>(sendboxMessages);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving sent messages. Please try again later.", ex);
        }
    }

    public async Task UpdateMessageAsync(UpdateMessageDto updateMessageDto)
    {
        try
        {
            var message = _mapper.Map<UserMessage>(updateMessageDto);
            _messageContext.UserMessages.Update(message);
            await _messageContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("An error occurred while updating the message. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while updating the message.", ex);
        }
    }

    public async Task<int> GetTotalMessageCount()
    {
        try
        {
            int totalMessageCount = await _messageContext.UserMessages.CountAsync();
            return totalMessageCount;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the total message count. Please try again later.", ex);
        }
    }
}
