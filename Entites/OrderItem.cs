namespace ShopApi.Entites;

public class OrderItem
{
    public int Id { get; set; } // PK
    public int OrderId { get; set; } // FK -> Orders
    public int ProductId { get; set; } // FK -> Products
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; } // Quantity * Product.Price

    public Order Order { get; set; }
    public Product Product { get; set; }
}
