using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class GetAddressQueryHandler
{
    private readonly IRepository<Address> _addressRepository;

    public GetAddressQueryHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<List<GetAddressQueryResult>> Handle()
    {
        try
        {
            var addressList = await _addressRepository.GetAllAsync();

            if (addressList == null || !addressList.Any())
            {
                return new List<GetAddressQueryResult>();
            }

            return addressList.Select(address => new GetAddressQueryResult
            {
                AddressId = address.AddressId,
                City = address.City,
                Detail = address.Detail1,
                District = address.District,
                UserId = address.UserId
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving addresses.", ex);
        }
    }
}
