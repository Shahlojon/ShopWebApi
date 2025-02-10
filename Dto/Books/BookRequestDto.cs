namespace Library.Dto.Books;

public class BookRequestDto
{
    public string Title { get; set; }  // 📚 Название книги
    public int AuthorId { get; set; } // 🔗 ID автора
    public string ISBN { get; set; }  // 📖 ISBN книги
}
