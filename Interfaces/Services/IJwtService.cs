using ShopApi.Entites;

namespace ShopApi.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}
