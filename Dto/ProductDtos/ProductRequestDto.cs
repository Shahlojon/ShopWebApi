namespace ShopApi.DTO.ProductDtos;

public class ProductRequestDto
{
    public string Name{ get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public List<IFormFile>? Files { get; set; }
}