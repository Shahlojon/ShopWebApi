namespace ShopApi.Entites;

public class ProductFile:DataFile
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
}