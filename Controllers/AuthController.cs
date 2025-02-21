using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dto.Authorize;
using ShopApi.Enums;
using ShopApi.Interfaces.Services;
using ShopApi.Responses;

namespace Presentation.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.AuthenticateAsync(request.Email, request.Password);
        if (token == null)
            return Unauthorized();

        return Ok(new ApiResponse<string>(token, "Успешняя авторизация"));
    }

    [HttpPost("register/user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
    {
        var success = await _authService.RegisterAsync(request.Email, request.Password, Role.User, request.FirstName, request.LastName);
        if (!success)
            return BadRequest(new ApiResponse<string>("Пользователь с таким именем уже существует"));

        return Ok(new ApiResponse<bool>(success,"Регистрация успешна"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("register/admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
    {
        var success = await _authService.RegisterAsync(request.Email, request.Password, Role.Admin, request.FirstName, request.LastName);
        if (!success)
            return BadRequest(new ApiResponse<string>("Ошибка при регистрации администратора"));

        return Ok(new ApiResponse<bool>(success, "Администратор успешно зарегистрирован"));
    }
}
