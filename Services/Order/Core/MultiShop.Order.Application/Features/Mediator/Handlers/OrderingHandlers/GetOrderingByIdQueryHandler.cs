using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;

public class GetOrderingByIdQueryHandler : IRequestHandler<GetOrderingByIdQuery, GetOrderingByIdQueryResult>
{
    private readonly IRepository<Ordering> _orderingRepository;

    public GetOrderingByIdQueryHandler(IRepository<Ordering> orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task<GetOrderingByIdQueryResult> Handle(GetOrderingByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var ordering = await _orderingRepository.GetByIdAsync(query.Id);

            if (ordering == null)
                throw new ApplicationException($"Ordering with ID {query.Id} was not found.");

            return new GetOrderingByIdQueryResult
            {
                OrderingId = ordering.OrderingId,
                OrderDate = ordering.OrderDate,
                TotalPrice = ordering.TotalPrice,
                UserId = ordering.UserId
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the order by ID.", ex);
        }
    }
}
