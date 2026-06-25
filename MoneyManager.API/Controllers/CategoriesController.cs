using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.Interfaces;
using MoneyManager.Application.DTOs;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
private readonly ICategoryRepository _categoryRepository;

public CategoriesController(ICategoryRepository categoryRepository)
{
    _categoryRepository = categoryRepository;
}

[HttpGet]
public async Task<IActionResult> GetAll()
{
    var categories = await _categoryRepository.GetAllAsync();

    return Ok(categories);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
var category = await _categoryRepository.GetByIdAsync(id);

if (category == null)
{
    return NotFound();
}

return Ok(category);

}

[HttpPost]
public async Task<IActionResult> Create(CreateCategoryDto dto)
{
    var category = new Category
    {
        Name = dto.Name,
        Description = dto.Description,
        Type = CategoryType.Expense,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        IsActive = true,

        // Utilisateur fixe pour les tests
        UserId = "55c52b21-765e-4fb2-b163-9562de696068"
    };

    await _categoryRepository.AddAsync(category);

    return CreatedAtAction(
        nameof(GetAll),
        new { id = category.Id },
        category);
}

[HttpPut("{id}")]
public async Task<IActionResult> Update(
int id,
UpdateCategoryDto dto)
{
var category = await _categoryRepository.GetByIdAsync(id);

if (category == null)
{
    return NotFound();
}

category.Name = dto.Name;
category.Description = dto.Description;
category.UpdatedAt = DateTime.UtcNow;

await _categoryRepository.UpdateAsync(category);

return NoContent();

}

[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
var category = await _categoryRepository.GetByIdAsync(id);

if (category == null)
{
    return NotFound();
}

await _categoryRepository.DeleteAsync(category);

return NoContent();

}
}
