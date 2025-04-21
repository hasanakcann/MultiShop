using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers;

public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommand>
{
    private readonly IRepository<Ordering> _orderingRepository;

    public UpdateOrderingCommandHandler(IRepository<Ordering> orderingRepository)
    {
        _orderingRepository = orderingRepository;
    }

    public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var orderingToUpdate = await _orderingRepository.GetByIdAsync(request.OrderingId);

            if (orderingToUpdate == null)
                throw new ApplicationException($"Ordering with ID {request.OrderingId} not found.");

            orderingToUpdate.UserId = request.UserId;
            orderingToUpdate.TotalPrice = request.TotalPrice;
            orderingToUpdate.OrderDate = request.OrderDate;

            await _orderingRepository.UpdateAsync(orderingToUpdate);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the order.", ex);
        }
    }
}
