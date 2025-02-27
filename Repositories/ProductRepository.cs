using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Entites;
using ShopApi.Exceptions;
using ShopApi.Extensions;
using ShopApi.Interfaces.Repositories;

namespace ShopApi.Repositories;

public class ProductRepository(ProductDbContext context): IProductRepository
{
    public async Task<Product> GetByIdAsync(int id)
    {
        return await context.Products
                   .Include(x => x.Category)
                   .Include(p=>p.ProductFiles)
                   .FirstOrDefaultAsync(p => p.Id == id) ??
               throw new NotFoundException($"Product for id {id} not found");
    }

    public async Task<IEnumerable<Product>> GetAllAsync(string? name, int? categoryId, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
    {
        var query = context.Products
            .Include(x => x.Category)
            .Include(p=>p.ProductFiles)
            .AsQueryable();
        query = query
            .WhereIf(!string.IsNullOrEmpty(name), x => x.Name.ToLower().Contains(name.ToLower()))
            .WhereIf(categoryId.HasValue, x => x.CategoryId == categoryId)
            .WhereIf(minPrice.HasValue, x => x.Price >= minPrice)
            .WhereIf(maxPrice.HasValue, x => x.Price <= maxPrice);
        var product = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return product;
    }

    public async Task CreateAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        await context.Products.Where(p => p.Id == product.Id).ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}