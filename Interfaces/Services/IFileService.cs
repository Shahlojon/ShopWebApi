using ShopApi.Dto;

namespace ShopApi.Interfaces.Services;

public interface IFileService
{
    Task RemoveAsync(string fileName);
    Task<FileDto> SaveAsync(IFormFile file);
}