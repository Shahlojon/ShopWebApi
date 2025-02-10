using Library.Context;
using Library.Dto.Books;
using Library.Models;
using Library.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly LibraryDbContext _context; // Контекст базы данных

    public BookController(LibraryDbContext context)
    {
        _context = context;
    }

    // ✅ Получить все книги с авторами
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<BookResponseDto>>>> GetAllBooks()
    {
        var books = await _context.Books
            .Include(b => b.Author) // Загружаем автора
            .ToListAsync();

        var result = books.Select(book => new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.Name, // 👤 Имя автора
            ISBN = book.ISBN
        }).ToList();

        return Ok(new ApiResponse<List<BookResponseDto>>(result, "Список книг успешно получен."));
    }

    // ✅ Получить книгу по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<BookResponseDto>>> GetBookById(int id)
    {
        var book = await _context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return NotFound(new ApiResponse<BookResponseDto>("Книга не найдена."));

        var result = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.Name,
            ISBN = book.ISBN
        };

        return Ok(new ApiResponse<BookResponseDto>(result, "Книга успешно найдена."));
    }

    // ✅ Добавить книгу
    [HttpPost]
    public async Task<ActionResult<ApiResponse<BookResponseDto>>> CreateBook([FromBody] BookRequestDto bookDto)
    {
        var author = await _context.Authors.FindAsync(bookDto.AuthorId);
        if (author == null)
            return BadRequest(new ApiResponse<BookResponseDto>("Автор не найден."));

        var book = new Book
        {
            Title = bookDto.Title,
            AuthorId = bookDto.AuthorId,
            Author = author,
            ISBN = bookDto.ISBN
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var response = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = author.Name
        };

        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, new ApiResponse<BookResponseDto>(response, "Книга успешно добавлена."));
    }
}

