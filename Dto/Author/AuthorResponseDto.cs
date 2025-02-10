namespace Library.Dto.Author;

public class AuthorResponseDto  // DTO для ответа с автором
{
    public int Id { get; set; } // 🔑 ID автора
    public string Name { get; set; } = string.Empty;  // 👤 Имя автора
    public List<string> BookTitles { get; set; } = new(); // 🔗 Названия книг автора
}
