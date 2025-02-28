using ShopApi.Dto;
using ShopApi.Dto.Magazine.CategoryDtos;
using ShopApi.Entites;

namespace ShopApi.DTO.ProductDtos;

public class ProductFilterResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public CategoryDto Category { get; set; }
    public List<FileDto> Files { get; set; }
}