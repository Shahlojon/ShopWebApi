namespace ShopApi.Dto.OrderDtos;

public class OrderItemRequestDto
{
    public int ProductId { get; set; } // FK -> Products
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; } // Quantity * Product.Price
}
