using ShopApi.Dto;
using ShopApi.Dto.Magazine.CategoryDtos;
using ShopApi.DTO.ProductDtos;
using ShopApi.Entites;
using ShopApi.Interfaces;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class ProductService(IProductRepository productRepository, IProductFileRepository fileRepository, IFileService fileService):IProductService
{
    public async Task<ProductFilterResponseDto> GetProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return new ProductFilterResponseDto();
    }

    public async Task<IEnumerable<ProductFilterResponseDto>> GetProductsAsync(GetProductFilterRequestQuery requestQuery)
    {
        var products = await productRepository.GetAllAsync(requestQuery.Name, requestQuery.CategoryId, requestQuery.MinPrice, requestQuery.MaxPrice, requestQuery.Page, requestQuery.PageSize);
        return products.Select(product => new ProductFilterResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = new CategoryDto
            {
                Id = product.Category.Id,
                Name = product.Category.Name,
                ParentCategoryId = product.Category.ParentCategoryId
            }
        });
    }

    public async Task<bool> CreateProductAsync(ProductRequestDto request)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId,
            Description = request.Description 
        };
        await productRepository.CreateAsync(product);

        if (request.Files != null || request.Files.Count > 0)
        {
            List<ProductFile> productFile = new();
            foreach (var file in request.Files)
            {
                FileDto fileDto = await fileService.SaveAsync(file);
                productFile.Add(new ProductFile
                {
                    Name = fileDto.Name,
                    Url = fileDto.Url,
                    Extension = fileDto.Extension,
                    Size = fileDto.Size,
                    ProductId = product.Id
                });
            }

            await fileRepository.AddAsync(productFile);
        }
        return true;
    }
  

    public async Task<bool> UpdateProductAsync(int id, ProductRequestDto product)
    {
        var productToUpdate = await productRepository.GetByIdAsync(id);
        if (productToUpdate == null) return false;

        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.CategoryId = product.CategoryId;
        productToUpdate.Description = product.Description;

        await productRepository.UpdateAsync(productToUpdate);

        // Обновление файлов
        if (product.Files != null && product.Files.Count > 0)
        {
        
            // Удаляем старые файлы, если они отсутствуют в новом списке
            var filesToDelete = productToUpdate.ProductFiles.Where(f => !product.Files.Any(newFile => newFile.FileName == f.Name)).ToList();
            if (filesToDelete.Count > 0)
            {
                foreach (var oldFile in filesToDelete)
                {
                    await fileRepository.DeleteAsync(oldFile);
                    await fileService.RemoveAsync(oldFile.Url);
                }
            }

            List<ProductFile> newFiles = new();
            foreach (var file in product.Files)
            {
                if (!productToUpdate.ProductFiles.Any(f => f.Name == file.FileName)) // Проверяем, если файл новый
                {
                    FileDto fileDto = await fileService.SaveAsync(file);
                    newFiles.Add(new ProductFile
                    {
                        Name = fileDto.Name,
                        Url = fileDto.Url,
                        Extension = fileDto.Extension,
                        Size = fileDto.Size,
                        ProductId = productToUpdate.Id
                    });
                }
            }

            if (newFiles.Count > 0)
            {
                await fileRepository.AddAsync(newFiles);
            }
        }

        return true;
    }


    public async Task<bool> DeleteProductAsync(int id)
    {
        var productToDelete = await productRepository.GetByIdAsync(id);
        await productRepository.DeleteAsync(productToDelete);
        if (productToDelete.ProductFiles.Count > 0)
        {
            foreach (var oldFile in productToDelete.ProductFiles)
            {
                await fileRepository.DeleteAsync(oldFile);
                await fileService.RemoveAsync(oldFile.Url);
            }
        }
        return true;
    }
}