using System.ComponentModel.DataAnnotations;

namespace ShopApi.Dto.Authorize;

public class LoginRequest  // 
{
    [Required]  // Атрибут объязательное поле
    public string Email { get; set; } = string.Empty;  //  

    [Required]
    public string Password { get; set; } = string.Empty; // 
}

