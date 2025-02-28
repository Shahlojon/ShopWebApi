using ShopApi.Dto.OrderDtos;

namespace ShopApi.Dto.OrderDtos;

public class OrderRequestDto
{
    public int UserId { get; set; }
    public List<OrderItemRequestDto> Items { get; set; }
}