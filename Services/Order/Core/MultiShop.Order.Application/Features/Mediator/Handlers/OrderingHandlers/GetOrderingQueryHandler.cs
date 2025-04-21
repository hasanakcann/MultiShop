using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;

public class GetOrderingQueryHandler : IRequestHandler<GetOrderingQuery, List<GetOrderingQueryResult>>
{
    private readonly IRepository<Ordering> _orderingRepository;

    public GetOrderingQueryHandler(IRepository<Ordering> orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task<List<GetOrderingQueryResult>> Handle(GetOrderingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orderings = await _orderingRepository.GetAllAsync();

            if (orderings == null || !orderings.Any())
                return new List<GetOrderingQueryResult>();

            var result = orderings.Select(ordering => new GetOrderingQueryResult
            {
                OrderingId = ordering.OrderingId,
                OrderDate = ordering.OrderDate,
                TotalPrice = ordering.TotalPrice,
                UserId = ordering.UserId
            }).ToList();

            return result;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the orders.", ex);
        }
    }
}
