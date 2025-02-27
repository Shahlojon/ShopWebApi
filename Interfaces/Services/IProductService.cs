
using ShopApi.DTO.ProductDtos;

namespace ShopApi.Interfaces;

public interface IProductService
{
    Task<ProductFilterResponseDto> GetProductAsync(int id);
    Task<IEnumerable<ProductFilterResponseDto>> GetProductsAsync(GetProductFilterRequestQuery requestQuery);
    Task<bool> CreateProductAsync(ProductRequestDto product);
    Task<bool> UpdateProductAsync(int id, ProductRequestDto product);
    Task<bool> DeleteProductAsync(int id);
}