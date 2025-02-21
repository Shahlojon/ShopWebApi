using Microsoft.EntityFrameworkCore;
using ShopApi.Context;
using ShopApi.Entites;
using ShopApi.Interfaces.Repositories;

namespace ShopApi.Repositories;

public class UserRepository(ProductDbContext context) : IUserRepository
{
    public async Task<User> GetByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetFilteredUsersAsync(string? email, string? name)
    {
        return await context.Users
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .Where(u=>email!=null && u.Email.Contains(email))
            .Where(u=>name!=null && (u.FirstName.Contains(name) || u.LastName.Contains(name)))
            .ToListAsync();
    }

    public void Update(User user)
    {
        context.Users.Update(user);
        context.SaveChanges();
    }

    public async Task Delete(User user)
    {
        await context.Users.Where(u => u.Id == user.Id).ExecuteDeleteAsync();
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Email == username);
    }

    public async Task AddAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }
}