using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Entites;
using ShopApi.Interfaces.Repositories;

namespace ShopApi.Repositories;

public class UserProfileRepository(ProductDbContext dbContext):IUserProfileRepository
{
    public async Task AddAsync(UserProfile userProfile)
    {
        await dbContext.UserProfiles.AddAsync(userProfile);
        await dbContext.SaveChangesAsync();
    }

    public async Task<UserProfile> GetByUserIdAsync(int userId)
    {
        return await dbContext.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task<bool> ExistAsync(int userId)
    {
        return await dbContext.UserProfiles.AnyAsync(x => x.UserId == userId);
    }

    public async Task UpdateAsync(UserProfile userProfile)
    {
        dbContext.UserProfiles.Update(userProfile);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserProfile userProfile)
    {
        await dbContext.UserProfiles.Where(x => x.UserId == userProfile.UserId).ExecuteDeleteAsync();
        await dbContext.SaveChangesAsync();
    }
}