using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class RemoveAddressCommandHandler
{
    private readonly IRepository<Address> _addressRepository;

    public RemoveAddressCommandHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(RemoveAddressCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command), "RemoveAddressCommand cannot be null.");
        }

        var address = await _addressRepository.GetByIdAsync(command.Id);

        if (address == null)
        {
            throw new KeyNotFoundException($"Address with ID {command.Id} not found.");
        }

        try
        {
            await _addressRepository.DeleteAsync(address);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the address.", ex);
        }
    }
}
