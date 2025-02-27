using ShopApi.Entites;
using ShopApi.Enums;

namespace ShopApi.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetAllAsync(int userId);
    Task<IEnumerable<Order>> GetFilteredOrdersAsync(int? userId, Status? status);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task Delete(Order order);
}
