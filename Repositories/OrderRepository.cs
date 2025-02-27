using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Entites;
using ShopApi.Enums;
using ShopApi.Exceptions;
using ShopApi.Interfaces.Repositories;

namespace ShopApi.Repositories;

public class OrderRepository(ProductDbContext context) : IOrderRepository
{
    public async Task<Order> GetByIdAsync(int id)
    {
        var order = await context.Orders.Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
            throw new NotFoundException($"Заказ с ID {id} не найден.");
        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await context.Orders.Include(o => o.OrderItems).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int userId)
    {
        return await context.Orders
            .Include(o => o.OrderItems)
            .Where(o=>o.UserId==userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetFilteredOrdersAsync(int? userId, Status? status)
    {
        var query = context.Orders
            .Include(o => o.OrderItems)
            .AsQueryable();

        if (userId.HasValue)
            query = query.Where(o => o.UserId == userId.Value);

        if (status!=null)
            query = query.Where(o => o.Status == status);

        return await query.ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Order order)
    {
        await context.Orders.Where(x=>x.Id==order.Id)
            .Include(x=>x.OrderItems)
            .ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}
