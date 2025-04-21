using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class CreateOrderDetailCommandHandler
{
    private readonly IRepository<OrderDetail> _orderDetailRepository;

    public CreateOrderDetailCommandHandler(IRepository<OrderDetail> orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task Handle(CreateOrderDetailCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command), "CreateOrderDetailCommand cannot be null.");
        }

        if (command.ProductPrice < 0 || command.ProductAmount <= 0)
        {
            throw new ArgumentException("Invalid product price or amount.");
        }

        var newOrderDetail = new OrderDetail
        {
            OrderingId = command.OrderingId,
            ProductAmount = command.ProductAmount,
            ProductName = command.ProductName,
            ProductId = command.ProductId,
            ProductPrice = command.ProductPrice,
            ProductTotalPrice = command.ProductTotalPrice
        };

        try
        {
            await _orderDetailRepository.CreateAsync(newOrderDetail);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the order detail.", ex);
        }
    }
}
