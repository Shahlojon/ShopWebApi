using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IEnumerable<User>> GetFilteredUsersAsync(string? email, string? name);
    Task AddAsync(User user);
    void Update(User user);
    void Delete(User user);
}