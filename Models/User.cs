namespace Library.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Reader"; // "Reader", "Librarian", "Admin"

    public List<BorrowedBook> BorrowedBooks { get; set; } = new();  // Навигационное свойство
}