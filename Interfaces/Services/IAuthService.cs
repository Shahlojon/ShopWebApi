using ShopApi.Enums;

namespace ShopApi.Interfaces.Services;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string email, string password);
    Task<bool> RegisterAsync(string email, string password, Role role, string? firstName, string? lastName);
}