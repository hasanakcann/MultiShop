using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class UpdateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _orderDetailRepository;

    public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task Handle(UpdateOrderDetailCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command), "UpdateOrderDetailCommand cannot be null.");
        }

        try
        {
            var existingOrderDetail = await _orderDetailRepository.GetByIdAsync(command.OrderDetailId);

            if (existingOrderDetail == null)
            {
                throw new KeyNotFoundException($"Order detail with ID {command.OrderDetailId} not found.");
            }

            existingOrderDetail.OrderingId = command.OrderingId;
            existingOrderDetail.ProductId = command.ProductId;
            existingOrderDetail.ProductName = command.ProductName;
            existingOrderDetail.ProductPrice = command.ProductPrice;
            existingOrderDetail.ProductTotalPrice = command.ProductTotalPrice;
            existingOrderDetail.ProductAmount = command.ProductAmount;

            await _orderDetailRepository.UpdateAsync(existingOrderDetail);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the order detail.", ex);
        }
    }
}
