using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.DTOs.Budget;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using System.Security.Claims;

namespace MoneyManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BudgetController : ControllerBase
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetController(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    private string? UserId =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    // GET : api/Budget
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var budgets = await _budgetRepository.GetByUserIdAsync(UserId);

        return Ok(budgets);
    }

    // GET : api/Budget/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var budget = await _budgetRepository.GetByIdAsync(id);

        if (budget == null)
        {
            return NotFound();
        }

        return Ok(budget);
    }

    // POST : api/Budget
    [HttpPost]
    public async Task<IActionResult> Create(CreateBudgetDto dto)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var budget = new Budget
        {
            AmountLimit = dto.AmountLimit,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = UserId
        };

        await _budgetRepository.AddAsync(budget);

        return CreatedAtAction(
            nameof(GetById),
            new { id = budget.Id },
            budget);
    }

    // PUT : api/Budget/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBudgetDto dto)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var budget = await _budgetRepository.GetByIdAsync(id);

        if (budget == null)
        {
            return NotFound();
        }

        budget.AmountLimit = dto.AmountLimit;
        budget.StartDate = dto.StartDate;
        budget.EndDate = dto.EndDate;
        budget.CategoryId = dto.CategoryId;
        budget.UpdatedAt = DateTime.UtcNow;

        await _budgetRepository.UpdateAsync(budget);

        return NoContent();
    }

    // DELETE : api/Budget/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var budget = await _budgetRepository.GetByIdAsync(id);

        if (budget == null)
        {
            return NotFound();
        }

        await _budgetRepository.DeleteAsync(budget);

        return NoContent();
    }
}