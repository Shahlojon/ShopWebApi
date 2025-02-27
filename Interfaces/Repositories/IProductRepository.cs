using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);

    Task<IEnumerable<Product>> GetAllAsync(
        string? name, 
        int? categoryId, 
        decimal? minPrice, 
        decimal? maxPrice, 
        int page,
        int pageSize);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
}