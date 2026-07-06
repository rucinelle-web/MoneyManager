using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.DTOs;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;

namespace MoneyManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
private readonly IExpenseRepository _expenseRepository;

public ExpenseController(IExpenseRepository expenseRepository)
{
    _expenseRepository = expenseRepository;
}

// GET : api/Expense
[HttpGet]
public async Task<IActionResult> GetAll()
{
    var expenses = await _expenseRepository.GetAllAsync();

    return Ok(expenses);
}

// GET : api/Expense/5
[HttpGet("{id}")]
public async Task<IActionResult> GetById(int id)
{
    var expense = await _expenseRepository.GetByIdAsync(id);

    if (expense == null)
    {
        return NotFound();
    }

    return Ok(expense);
}

// POST : api/Expense
[HttpPost]
public async Task<IActionResult> Create(CreateExpenseDto dto)
{
    var expense = new Expense
    {
        Amount = dto.Amount,
        Description = dto.Description,
        ExpenseDate = dto.ExpenseDate,
        CategoryId = dto.CategoryId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,

        // Temporaire pour les tests
        UserId = "55c52b21-765e-4fb2-b163-9562de696068"
    };

    await _expenseRepository.AddAsync(expense);

    return CreatedAtAction(
        nameof(GetById),
        new { id = expense.Id },
        expense);
}

// PUT : api/Expense/5
[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, UpdateExpenseDto dto)
{
    var expense = await _expenseRepository.GetByIdAsync(id);

    if (expense == null)
    {
        return NotFound();
    }

    expense.Amount = dto.Amount;
    expense.Description = dto.Description;
    expense.ExpenseDate = dto.ExpenseDate;
    expense.CategoryId = dto.CategoryId;
    expense.UpdatedAt = DateTime.UtcNow;

    await _expenseRepository.UpdateAsync(expense);

    return NoContent();
}

// DELETE : api/Expense/5
[HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
{
    var expense = await _expenseRepository.GetByIdAsync(id);

    if (expense == null)
    {
        return NotFound();
    }

    await _expenseRepository.DeleteAsync(expense);

    return NoContent();
}

}
