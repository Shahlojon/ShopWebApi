namespace Library.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    // Связь один ко многим – коллекция продуктов
    public List<Book> Books { get; set; } = new();
}
