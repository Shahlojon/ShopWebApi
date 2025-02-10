using Library.Context;
using Library.Dto.Author;
using Library.Models;
using Library.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public AuthorsController(LibraryDbContext context)
    {
        _context = context;
    }

    // ✅ Получить всех авторов
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<AuthorResponseDto>>>> GetAllAuthors()
    {
        var authors = await _context.Authors
            .Include(a => a.Books) // Загружаем книги автора
            .ToListAsync();

        var result = authors.Select(author => new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BookTitles = author.Books.Select(b => b.Title).ToList() // 📚 Список книг автора
        }).ToList();

        return Ok(new ApiResponse<List<AuthorResponseDto>>(result, "Список авторов успешно получен."));
    }

    // ✅ Получить автора по ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<AuthorResponseDto>>> GetAuthorById(int id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
            return NotFound(new ApiResponse<AuthorResponseDto>("Автор не найден."));

        var result = new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BookTitles = author.Books.Select(b => b.Title).ToList()
        };

        return Ok(new ApiResponse<AuthorResponseDto>(result, "Автор успешно найден."));
    }

    // ✅ Добавить нового автора
    [HttpPost]
    public async Task<ActionResult<ApiResponse<AuthorResponseDto>>> CreateAuthor([FromBody] AuthorRequestDto authorDto)
    {
        var author = new Author
        {
            Name = authorDto.Name
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        var response = new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BookTitles = new List<string>()
        };

        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, new ApiResponse<AuthorResponseDto>(response, "Автор успешно добавлен."));
    }

    // ✅ Обновить данные автора
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] AuthorRequestDto authorDto)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
            return NotFound(new ApiResponse<AuthorResponseDto>("Автор не найден."));

        author.Name = authorDto.Name;
        await _context.SaveChangesAsync();

        return Ok(new ApiResponse<AuthorResponseDto>(new AuthorResponseDto
        {
            Id = author.Id,
            Name = author.Name,
            BookTitles = _context.Books.Where(b => b.AuthorId == author.Id).Select(b => b.Title).ToList()
        }, "Автор успешно обновлён."));
    }

    // ✅ Удалить автора
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(Guid id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
            return NotFound(new ApiResponse<AuthorResponseDto>("Автор не найден."));

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return Ok(new ApiResponse<string>("Автор успешно удалён."));
    }
}

