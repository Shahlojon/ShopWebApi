using ShopApi.Dto.Magazine.CategoryDtos;

namespace ShopApi.Interfaces.Services;

public interface ICategoryService
{
    Task<CategoryDto> GetByIdAsync(int id);
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<IEnumerable<CategoryDto>> GetSubcategoriesAsync(int parentId);
    Task<CategoryWithSubcategoriesDto> GetCategoryWithSubcategoriesAsync(int id);
    Task<int> CreateAsync(CategoryCreateDto dto);
    Task<bool> UpdateAsync(int id, CategoryUpdateDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<CategoryTreeDto>> GetCategoryTreeAsync();
}
