using ShopApi.Entites;
using ShopApi.Enums;

namespace ShopApi.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetFilteredOrdersAsync(Guid? userId, Status status);
    Task AddAsync(Order order);
    void Update(Order order);
    void Delete(Order order);
}
