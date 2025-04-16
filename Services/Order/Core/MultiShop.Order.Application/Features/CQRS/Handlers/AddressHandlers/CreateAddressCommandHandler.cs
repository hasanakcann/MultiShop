using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class CreateAddressCommandHandler
{
    private readonly IRepository<Address> _addressRepository;

    public CreateAddressCommandHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task Handle(CreateAddressCommand command)
    {
        var newAddress = new Address
        {
            UserId = command.UserId,
            Name = command.Name,
            Surname = command.Surname,
            Email = command.Email,
            Phone = command.Phone,
            Country = command.Country,
            City = command.City,
            District = command.District,
            ZipCode = command.ZipCode,
            Detail1 = command.Detail1,
            Detail2 = command.Detail2
        };

        await _addressRepository.CreateAsync(newAddress);
    }
}
