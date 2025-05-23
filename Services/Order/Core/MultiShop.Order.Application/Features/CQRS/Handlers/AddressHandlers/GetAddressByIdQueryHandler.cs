﻿using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers;

public class GetAddressByIdQueryHandler
{
    private readonly IRepository<Address> _addressRepository;

    public GetAddressByIdQueryHandler(IRepository<Address> addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery query)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query), "Query cannot be null.");
        }

        var address = await _addressRepository.GetByIdAsync(query.Id);

        if (address == null)
        {
            throw new KeyNotFoundException($"Address with ID {query.Id} not found.");
        }

        return new GetAddressByIdQueryResult
        {
            AddressId = address.AddressId,
            City = address.City,
            Detail = address.Detail1,
            District = address.District,
            UserId = address.UserId
        };
    }
}
