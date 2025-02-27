using ShopApi.Dto.OrderDtos;
using ShopApi.Entites;
using ShopApi.Enums;
using ShopApi.Exceptions;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository) : IOrderService
{
    private readonly IOrderRepository _orderRepository=orderRepository;
    private readonly IOrderItemRepository _orderItemRepository=orderItemRepository;
    public async Task<bool> CreateAsync(int userId, Status status, List<OrderItemRequestDto> orderItemRequest)
    {
        Order order = new()
        {
            UserId = userId,
            Status = status,
        };
        await _orderRepository.AddAsync(order);
        await _orderItemRepository.CreateAsync(orderItemRequest.Select(oI => new OrderItem
        {
            OrderId = userId,
            ProductId = oI.ProductId,
            Quantity = oI.Quantity,
            TotalPrice = oI.TotalPrice,
        }).ToList());
        return true;
    }

    public async Task<IEnumerable<OrderDto>> GetListAsync(int userId)
    {
        if (userId <= 0) throw new NotFoundException("Не корректные данные");
        var orders =  await _orderRepository.GetAllAsync(userId);
        return orders.Select(o => new OrderDto
        {
            Status = o.Status,
            Id = o.Id,
            CreatedAt = o.CreatedAt,
            UserId = userId,
            User = new Dto.UserDtos.UserDto
            {
                Id = userId,
                Email = o.User.Email,
                FirstName = o.User.FirstName,
                LastName = o.User.LastName,

            }
        });
    }
}
