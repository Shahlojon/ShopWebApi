namespace ShopApi.Entites;

public class Product
{
    public int Id { get; set; } // PK
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; } // FK -> Categories
    public int Stock { get; set; } 

    public Category Category { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    
    public ICollection<ProductFile> ProductFiles { get; set; } = new List<ProductFile>();
}
