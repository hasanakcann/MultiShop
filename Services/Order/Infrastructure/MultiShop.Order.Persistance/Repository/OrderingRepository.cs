using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using MultiShop.Order.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace MultiShop.Order.Persistance.Repository;

public class OrderingRepository : IOrderingRepository
{
    private readonly OrderContext _orderContext;

    public OrderingRepository(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }

    public async Task<List<Ordering>> GetOrderingsByUserIdAsync(string userId)
    {
        var orderings = await _orderContext.Orderings
            .Where(x => x.UserId == userId)
            .ToListAsync();
        return orderings;
    }
}
