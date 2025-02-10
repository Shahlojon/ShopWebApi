namespace Library.Dto.Books;

public class BookResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty; // 👤 Имя автора
    public string ISBN { get; set; }
}
