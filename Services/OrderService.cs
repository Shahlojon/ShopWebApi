using ShopApi.Dto.OrderDtos;
using ShopApi.Dto.UserDtos;
using ShopApi.Entites;
using ShopApi.Enums;
using ShopApi.Exceptions;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class OrderService(IOrderRepository orderRepository, IUserRepository userRepository,
    IProductRepository productRepository):IOrderService
{
    public async Task<OrderResponseDto> GetOrderByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id) ?? throw new NotFoundException($"id");
        return MapToOrderResponseDto(order);
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders.Select(MapToOrderResponseDto).ToList();
    }

    public async Task<int> CreateOrderAsync(OrderRequestDto orderDto)
    {
        var user = await userRepository.GetByIdAsync(orderDto.UserId);
        if (user is null) 
             throw new NotFoundException($"Пользователь с ID {orderDto.UserId} не найден.");

        if (orderDto.Items == null || !orderDto.Items.Any())
            throw new ValidationException("Заказ должен содержать хотя бы один товар.");

        var order = new Order
        {
            UserId = orderDto.UserId,
            OrderItems = orderDto.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice,
            }).ToList()
        };

        foreach (var item in orderDto.Items)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId);
            if (product == null)
                throw new NotFoundException($"Товар с ID {item.ProductId} не найден.");

            if (product.Stock < item.Quantity)
                throw new ValidationException($"Недостаточно товара {product.Name} на складе.");

            product.Stock -= item.Quantity; // Обновляем склад

            order.OrderItems.Add(new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = item.Quantity,
                TotalPrice = product.Price * item.Quantity
            });
        }

        await orderRepository.AddAsync(order);
        return order.Id;
    }

    

    public async Task<bool> UpdateOrderStatusAsync(int id, Status status)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new NotFoundException($"Заказ с ID {id} не найден.");

        //if (!new[] { "Pending", "Paid", "Shipped", "Cancelled" }.Contains(status))
          //  throw new ValidationException("Недопустимый статус заказа.");

        order.Status = status;
        await orderRepository.UpdateAsync(order);
        return true;
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
            throw new NotFoundException($"Заказ с ID {id} не найден.");

        await orderRepository.Delete(order);
        return true;
    }

    private OrderResponseDto MapToOrderResponseDto(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            Status = order.Status,
            UserResponse = new UserResponseDto()
            {
                Id = order.User.Id,
                FirstName = order.User.FirstName,
                Email = order.User.Email,
                Role = order.User.Role
            },
            Items = order.OrderItems.Select(item => new OrderItemResponseDto()
            {
                ProductId = item.Product.Id,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice
            }).ToList()
        };
    }
}