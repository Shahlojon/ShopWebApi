namespace Library.Models;

public class BorrowedBook  // 📚 Книга, взятая на руки
{
    public int Id { get; set; }  // 🔑 ID книги
    public int UserId { get; set; }  // 🔗 ID пользователя
    public int BookId { get; set; }  // 🔗 ID книги
    public DateTime BorrowedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReturnedAt { get; set; } // null, если книга ещё не возвращена

    public User User { get; set; } = null!;  // Навигационное свойство
    public Book Book { get; set; } = null!;  // Навигационное свойство
}