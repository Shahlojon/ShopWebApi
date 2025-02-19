namespace ShopApi.Entites;

public class Category
{
    public int Id { get; set; } // PK
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; } // FK -> Categories (self-referencing)

    public Category ParentCategory { get; set; }
    public ICollection<Category> Subcategories { get; set; } = new List<Category>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

