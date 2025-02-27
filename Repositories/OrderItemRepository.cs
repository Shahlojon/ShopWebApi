using ShopApi.Context;
using ShopApi.Entites;
using ShopApi.Interfaces.Repositories;

namespace ShopApi.Repositories;

public class OrderItemRepository(ProductDbContext context) : IOrderItemRepository
{
    private readonly ProductDbContext _context=context;
    public async Task<bool> CreateAsync(List<OrderItem> items)
    {
        await _context.OrderItems.AddRangeAsync(items);
        await _context.SaveChangesAsync();
        return true;
    }
}
