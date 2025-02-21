using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetFilteredUsersAsync(string? email, string? name);
    Task AddAsync(User user);
    void Update(User user);
    Task Delete(User user);
    
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsAsync(string username);
}