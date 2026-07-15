using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.DTOs.Income;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using System.Security.Claims;

namespace MoneyManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeRepository _incomeRepository;

    public IncomeController(IIncomeRepository incomeRepository)
    {
        _incomeRepository = incomeRepository;
    }

    private string? UserId =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    // GET : api/Income
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var incomes = await _incomeRepository.GetByUserIdAsync(UserId);

        return Ok(incomes);
    }

    // GET : api/Income/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

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
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var income = new Income
        {
            Amount = dto.Amount,
            Description = dto.Description,
            IncomeDate = dto.IncomeDate,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = UserId
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
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

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
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var income = await _incomeRepository.GetByIdAsync(id);

        if (income == null)
        {
            return NotFound();
        }

        await _incomeRepository.DeleteAsync(income);

        return NoContent();
    }
}