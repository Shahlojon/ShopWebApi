using System.Collections;
using ShopApi.Dto.OrderDtos;
using ShopApi.Enums;

namespace ShopApi.Interfaces.Services;

public interface IOrderService
{
    Task<OrderResponseDto> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<int> CreateOrderAsync(OrderRequestDto orderDto);
    Task<bool> UpdateOrderStatusAsync(int id, Status status);
    Task<bool> DeleteOrderAsync(int id);
}