namespace ShopApi.Dto.Magazine.CategoryDtos;

public class CategoryWithSubcategoriesDto : CategoryDto
{
    public List<CategoryDto> Subcategories { get; set; }
}