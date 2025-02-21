using Microsoft.AspNetCore.Authorization;
using ShopApi.Responses;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Dto.Magazine.CategoryDtos;
using ShopApi.Interfaces.Services;

namespace ShopApi.Controllers;

[Route("api/categories")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Получить все категории
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<CategoryDto>>(categories));
    }

    /// <summary>
    /// Получить категорию по ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return category != null ? Ok(new ApiResponse<CategoryDto>(category)) : NotFound(new ApiResponse<string>($"Категория с ID {id} не найдена."));
    }

    /// <summary>
    /// Получить все подкатегории по ID родительской категории
    /// </summary>
    [HttpGet("{id:int}/subcategories")]
    public async Task<IActionResult> GetSubcategories(int id)
    {
        var subcategories = await _categoryService.GetSubcategoriesAsync(id);
        return Ok(new ApiResponse<IEnumerable<CategoryDto>>(subcategories));
    }

    /// <summary>
    /// Получить категорию с вложенными подкатегориями
    /// </summary>
    [HttpGet("{id:int}/tree")]
    public async Task<IActionResult> GetCategoryWithSubcategories(int id)
    {
        var category = await _categoryService.GetCategoryWithSubcategoriesAsync(id);
        return category != null ? Ok(new ApiResponse<CategoryWithSubcategoriesDto>(category)) : NotFound(new ApiResponse<string>($"Категория с ID {id} не найдена."));
    }

    [HttpGet("tree")]
    public async Task<IActionResult> GetCategорoryTree()
    {
        var categoryTree = await _categoryService.GetCategoryTreeAsync();
        return Ok(new ApiResponse<List<CategoryTreeDto>>(categoryTree));
    }

    /// <summary>
    /// Создать категорию
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var categoryId = await _categoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = categoryId }, new { Id = categoryId });
    }

    /// <summary>
    /// Обновить категорию по ID
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _categoryService.UpdateAsync(id, dto);
        return updated ? NoContent() : NotFound(new ApiResponse<string>($"Категория с ID {id} не найдена."));
    }

    /// <summary>
    /// Удалить категорию по ID
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new ApiResponse<string>($"Категория с ID {id} не найдена."));
    }
}
