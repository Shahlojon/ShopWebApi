using ShopApi.Dto.Magazine.CategoryDtos;
using ShopApi.Entites;
using ShopApi.Exceptions;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new NotFoundException($"Категория с ID {id} не найдена.");

        return new CategoryDto { Id = category.Id, Name = category.Name, ParentCategoryId = category.ParentCategoryId };
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name, ParentCategoryId = c.ParentCategoryId }).ToList();
    }

    public async Task<IEnumerable<CategoryDto>> GetSubcategoriesAsync(int parentId)
    {
        var subcategories = await _categoryRepository.GetSubcategoriesAsync(parentId);
        return subcategories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name, ParentCategoryId = c.ParentCategoryId }).ToList();
    }

    public async Task<CategoryWithSubcategoriesDto> GetCategoryWithSubcategoriesAsync(int id)
    {
        var category = await _categoryRepository.GetCategoryWithSubcategoriesAsync(id);
        if (category == null)
            throw new NotFoundException($"Категория с ID {id} не найдена.");

        return new CategoryWithSubcategoriesDto
        {
            Id = category.Id,
            Name = category.Name,
            Subcategories = category.Subcategories.Select(sc => new CategoryDto { Id = sc.Id, Name = sc.Name, ParentCategoryId = sc.ParentCategoryId }).ToList()
        };
    }

    public async Task<int> CreateAsync(CategoryCreateDto dto)
    {
        // Если parentCategoryId = 0, значит, это корневая категория
        int? parentId = dto.ParentCategoryId > 0 ? dto.ParentCategoryId : null;

        // Проверяем, существует ли родительская категория (если parentId не null)
        if (parentId.HasValue)
        {
            var parentExists = await _categoryRepository.ExistsAsync(parentId.Value);
            if (!parentExists)
            {
                throw new Exception($"Родительская категория с ID {parentId} не найдена.");
            }
        }

        var category = new Category
        {
            Name = dto.Name,
            ParentCategoryId = parentId
        };
        await _categoryRepository.AddAsync(category);
        return category.Id;
    }
    public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new NotFoundException($"Категория с ID {id} не найдена.");

        category.Name = dto.Name;
        category.ParentCategoryId = dto.ParentCategoryId;
        _categoryRepository.Update(category);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new NotFoundException($"Категория с ID {id} не найдена.");

        _categoryRepository.Delete(category);
        return true;
    }
    
    public async Task<List<CategoryTreeDto>> GetCategoryTreeAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        // Группируем категории по родительскому ID
        var categoryLookup = categories.ToLookup(c => c.ParentCategoryId);

        // Метод для рекурсивной сборки дерева
        List<CategoryTreeDto> BuildTree(int? parentId)
        {
            return categoryLookup[parentId]
                .Select(c => new CategoryTreeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subcategories = BuildTree(c.Id)
                })
                .ToList();
        }

        return BuildTree(null); // Начинаем с корневых категорий
    }

}
