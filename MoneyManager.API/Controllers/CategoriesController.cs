using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.DTOs.Category;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using MoneyManager.Domain.Enums;
using System.Security.Claims;

namespace MoneyManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    private string? UserId =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    // GET : api/Categories
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var categories = await _categoryRepository.GetByUserIdAsync(UserId);

        return Ok(categories);
    }

    // GET : api/Categories/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    // POST : api/Categories
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            Type = CategoryType.Expense,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            UserId = UserId
        };

        await _categoryRepository.AddAsync(category);

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            category);
    }

    // PUT : api/Categories/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

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

    // DELETE : api/Categories/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        await _categoryRepository.DeleteAsync(category);

        return NoContent();
    }
}