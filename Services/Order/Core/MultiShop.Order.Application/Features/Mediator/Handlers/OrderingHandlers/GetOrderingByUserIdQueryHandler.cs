using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;

public class GetOrderingByUserIdQueryHandler : IRequestHandler<GetOrderingByUserIdQuery, List<GetOrderingByUserIdQueryResult>>
{
    private readonly IOrderingRepository _orderingRepository;

    public GetOrderingByUserIdQueryHandler(IOrderingRepository orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task<List<GetOrderingByUserIdQueryResult>> Handle(GetOrderingByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderings = await _orderingRepository.GetOrderingsByUserIdAsync(request.Id);

            if (orderings == null || !orderings.Any())
                throw new ApplicationException($"No orders found for user with ID {request.Id}.");

            return orderings.Select(order => new GetOrderingByUserIdQueryResult
            {
                OrderingId = order.OrderingId,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving orders for the user.", ex);
        }
    }
}
