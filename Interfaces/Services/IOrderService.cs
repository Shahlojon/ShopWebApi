using System.Collections;
using ShopApi.Dto.OrderDtos;
using ShopApi.Enums;

namespace ShopApi.Interfaces.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetListAsync(int userId);
    Task<bool> CreateAsync(int userId, Status status, List<OrderItemRequestDto> orderItemRequest);
}