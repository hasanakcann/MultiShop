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
        var newOrderDetail = new OrderDetail
        {
            OrderingId = command.OrderingId,
            ProductAmount = command.ProductAmount,
            ProductName = command.ProductName,
            ProductId = command.ProductId,
            ProductPrice = command.ProductPrice,
            ProductTotalPrice = command.ProductTotalPrice
        };

        await _orderDetailRepository.CreateAsync(newOrderDetail);
    }
}
