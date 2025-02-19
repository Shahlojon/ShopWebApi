namespace ShopApi.Dto.Magazine.CategoryDtos;

public class CategoryTreeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CategoryTreeDto> Subcategories { get; set; } = new();
}
