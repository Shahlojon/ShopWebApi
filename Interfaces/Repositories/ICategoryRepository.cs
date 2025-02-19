using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<IEnumerable<Category>> GetSubcategoriesAsync(int parentId);
    Task<Category> GetCategoryWithSubcategoriesAsync(int id);
    Task AddAsync(Category category);
    void Update(Category category);
    void Delete(Category category);
    Task<bool> ExistsAsync(int id);
}

