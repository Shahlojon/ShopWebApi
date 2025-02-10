namespace Library.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;  // Название книги
    public int AuthorId { get; set; }  // Внешний ключ
    // Внешний ключ
    public Author Author { get; set; }// Навигационное свойство
    public string ISBN { get; set; } = string.Empty;

    public List<BorrowedBook> BorrowedBooks { get; set; } = new();
}
