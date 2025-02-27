using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<bool> CreateAsync(List<OrderItem> items);
}
