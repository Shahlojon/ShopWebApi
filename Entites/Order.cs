using ShopApi.Enums;

namespace ShopApi.Entites;

public class Order
{
    public int Id { get; set; } // PK
    public int UserId { get; set; } // FK -> Users
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Status Status { get; set; } = Status.Pending; // "Pending", "Shipped", "Completed", "Canceled"

    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}