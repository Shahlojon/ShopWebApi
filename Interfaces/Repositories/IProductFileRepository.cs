using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface IProductFileRepository
{
    Task AddAsync(List<ProductFile> productFile);
    Task<ProductFile> GetByProductIdAsync(int productFileId);
    Task UpdateAsync(ProductFile productFile);
    Task DeleteAsync(ProductFile productFile);
}