using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Entites;
using ShopApi.Interfaces.Repositories;

namespace ShopApi.Repositories;

public class ProductFileRepository(ProductDbContext context):IProductFileRepository
{
    public async Task AddAsync(List<ProductFile> productFiles)
    {
        await context.ProductFiles.AddRangeAsync(productFiles);
        await context.SaveChangesAsync();
    }

    public async Task<ProductFile> GetByProductIdAsync(int productFileId)
    {
        return await context.ProductFiles.FirstOrDefaultAsync(x => x.Id == productFileId);
    }

    public async Task UpdateAsync(ProductFile productFile)
    {
        context.ProductFiles.Update(productFile);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ProductFile productFile)
    {
        await context.ProductFiles.Where(x => x.Id == productFile.Id).ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }
}