using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class RemoveOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _orderDetailRepository;

    public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task Handle(RemoveOrderDetailCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command), "RemoveOrderDetailCommand cannot be null.");
        }

        try
        {
            var orderDetailToRemove = await _orderDetailRepository.GetByIdAsync(command.Id);

            if (orderDetailToRemove == null)
            {
                throw new KeyNotFoundException($"Order detail with ID {command.Id} not found.");
            }

            await _orderDetailRepository.DeleteAsync(orderDetailToRemove);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the order detail.", ex);
        }
    }
}
