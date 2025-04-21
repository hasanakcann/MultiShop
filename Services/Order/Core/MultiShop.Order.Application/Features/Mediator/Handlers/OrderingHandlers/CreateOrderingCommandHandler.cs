using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;

public class CreateOrderingCommandHandler : IRequestHandler<CreateOrderingCommand>
{
    private readonly IRepository<Ordering> _orderingRepository;

    public CreateOrderingCommandHandler(IRepository<Ordering> orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newOrdering = new Ordering
            {
                OrderDate = request.OrderDate,
                TotalPrice = request.TotalPrice,
                UserId = request.UserId
            };

            await _orderingRepository.CreateAsync(newOrdering);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the order.", ex);
        }
    }
}
