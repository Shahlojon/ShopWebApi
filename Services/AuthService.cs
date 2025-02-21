using ShopApi.Entites;
using ShopApi.Enums;
using ShopApi.Interfaces.Repositories;
using ShopApi.Interfaces.Services;

namespace ShopApi.Services;

public class AuthService : IAuthService // 
{
    private readonly IUserRepository _userRepository; // 
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string?> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        return _jwtService.GenerateToken(user);
    }

    public async Task<bool> RegisterAsync(string email, string password, Role role, string? firstName, string? lastName)
    {
        if (await _userRepository.ExistsAsync(email))
            return false;

        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        await _userRepository.AddAsync(user);
        return true;
    }
}

