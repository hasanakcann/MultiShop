using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;

public class GetOrderDetailByIdQueryHandler
{
    private readonly IRepository<OrderDetail> _orderDetailRepository;

    public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery query)
    {
        var orderDetail = await _orderDetailRepository.GetByIdAsync(query.Id);
        return new GetOrderDetailByIdQueryResult
        {
            OrderDetailId = orderDetail.OrderDetailId,
            OrderingId = orderDetail.OrderingId,
            ProductAmount = orderDetail.ProductAmount,
            ProductId = orderDetail.ProductId,
            ProductName = orderDetail.ProductName,
            ProductPrice = orderDetail.ProductPrice,
            ProductTotalPrice = orderDetail.ProductTotalPrice
        };
    }
}
