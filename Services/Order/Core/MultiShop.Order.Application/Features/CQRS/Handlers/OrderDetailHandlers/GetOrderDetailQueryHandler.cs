using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class GetOrderDetailQueryHandler
{
    private readonly IRepository<OrderDetail> _orderDetailRepository;

    public GetOrderDetailQueryHandler(IRepository<OrderDetail> orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<List<GetOrderDetailQueryResult>> Handle()
    {
        try
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();

            if (orderDetails == null || !orderDetails.Any())
            {
                return new List<GetOrderDetailQueryResult>();
            }

            return orderDetails.Select(order => new GetOrderDetailQueryResult
            {
                OrderDetailId = order.OrderDetailId,
                OrderingId = order.OrderingId,
                ProductAmount = order.ProductAmount,
                ProductId = order.ProductId,
                ProductName = order.ProductName,
                ProductPrice = order.ProductPrice,
                ProductTotalPrice = order.ProductTotalPrice
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving order details.", ex);
        }
    }
}
