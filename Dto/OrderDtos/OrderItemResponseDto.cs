namespace ShopApi.Dto.OrderDtos;

public class OrderItemResponseDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}