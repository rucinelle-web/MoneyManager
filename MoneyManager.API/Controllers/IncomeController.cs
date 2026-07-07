using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.DTOs;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeController : ControllerBase
{
private readonly IIncomeRepository _incomeRepository;


public IncomeController(IIncomeRepository incomeRepository)
{
    _incomeRepository = incomeRepository;
}

// GET : api/Income
[HttpGet]
public async Task<IActionResult> GetAll()
{
    var incomes = await _incomeRepository.GetAllAsync();

    return Ok(incomes);
}

// GET : api/Income/5
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var income = await _incomeRepository.GetByIdAsync(id);

    if (income == null)
    {
        return NotFound();
    }

    return Ok(income);
}

// POST : api/Income
[HttpPost]
public async Task<IActionResult> Create(CreateIncomeDto dto)
{
    var income = new Income
    {
        Amount = dto.Amount,
        Description = dto.Description,
        IncomeDate = dto.IncomeDate,
        CategoryId = dto.CategoryId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,

        // Temporaire pour les tests
        UserId = "55c52b21-765e-4fb2-b163-9562de696068"
    };

    await _incomeRepository.AddAsync(income);

    return CreatedAtAction(
        nameof(GetById),
        new { id = income.Id },
        income);
}

// PUT : api/Income/5
[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, UpdateIncomeDto dto)
{
    var income = await _incomeRepository.GetByIdAsync(id);

    if (income == null)
    {
        return NotFound();
    }

    income.Amount = dto.Amount;
    income.Description = dto.Description;
    income.IncomeDate = dto.IncomeDate;
    income.CategoryId = dto.CategoryId;
    income.UpdatedAt = DateTime.UtcNow;

    await _incomeRepository.UpdateAsync(income);

    return NoContent();
}

// DELETE : api/Income/5
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var income = await _incomeRepository.GetByIdAsync(id);

    if (income == null)
    {
        return NotFound();
    }

    await _incomeRepository.DeleteAsync(income);

    return NoContent();
}

}
