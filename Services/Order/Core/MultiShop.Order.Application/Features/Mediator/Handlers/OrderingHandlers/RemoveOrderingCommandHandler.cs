using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;

public class RemoveOrderingCommandHandler : IRequestHandler<RemoveOrderingCommand>
{
    private readonly IRepository<Ordering> _orderingRepository;

    public RemoveOrderingCommandHandler(IRepository<Ordering> orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderingToRemove = await _orderingRepository.GetByIdAsync(request.Id);

            if (orderingToRemove == null)
                throw new ApplicationException($"Ordering with ID {request.Id} not found.");

            await _orderingRepository.DeleteAsync(orderingToRemove);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while removing the order.", ex);
        }
    }
}
