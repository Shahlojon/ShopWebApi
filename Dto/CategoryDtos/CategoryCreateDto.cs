namespace ShopApi.Dto.Magazine.CategoryDtos;

public class CategoryCreateDto
{
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}