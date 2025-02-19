namespace ShopApi.Entites;

public class Review
{
    public int Id { get; set; } // PK
    public int UserId { get; set; } // FK -> Users
    public int ProductId { get; set; } // FK -> Products
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; }

    public User User { get; set; }
    public Product Product { get; set; }
}
