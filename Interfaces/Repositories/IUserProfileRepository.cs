using ShopApi.Entites;

namespace ShopApi.Interfaces.Repositories;

public interface IUserProfileRepository
{
    Task AddAsync(UserProfile userProfile);
    Task<UserProfile> GetByUserIdAsync(int userId);
    Task<bool> ExistAsync(int userId);
    Task UpdateAsync(UserProfile userProfile);
    Task DeleteAsync(UserProfile userProfile);
}