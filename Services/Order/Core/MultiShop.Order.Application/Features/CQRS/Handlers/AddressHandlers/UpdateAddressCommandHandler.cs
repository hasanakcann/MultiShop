using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class UpdateAddressCommandHandler
{
    private readonly IRepository<Address> _addressRepository;

    public UpdateAddressCommandHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(UpdateAddressCommand command)
    {
        var address = await _addressRepository.GetByIdAsync(command.AddressId);

        if (address == null)
        {
            throw new KeyNotFoundException($"Address with ID {command.AddressId} not found.");
        }

        address.Detail1 = command.Detail;
        address.District = command.District;
        address.City = command.City;
        address.UserId = command.UserId;

        try
        {
            await _addressRepository.UpdateAsync(address);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the address.", ex);
        }
    }
}
