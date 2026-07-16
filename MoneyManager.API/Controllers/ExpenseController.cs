using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.DTOs.Expense;
using MoneyManager.Application.Interfaces;
using System.Security.Claims;

namespace MoneyManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    private string? UserId =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    // GET : api/Expense
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (string.IsNullOrEmpty(UserId))
            return Unauthorized();

        var expenses = await _expenseService.GetAllAsync(UserId);

        return Ok(expenses);
    }

    // GET : api/Expense/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (string.IsNullOrEmpty(UserId))
            return Unauthorized();

        var expense = await _expenseService.GetByIdAsync(id, UserId);

        if (expense == null)
            return NotFound();

        return Ok(expense);
    }

    // POST : api/Expense
    [HttpPost]
    public async Task<IActionResult> Create(CreateExpenseDto dto)
    {
        if (string.IsNullOrEmpty(UserId))
            return Unauthorized();

        var expense = await _expenseService.CreateAsync(dto, UserId);

        return CreatedAtAction(
            nameof(GetById),
            new { id = expense.Id },
            expense);
    }

    // PUT : api/Expense/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateExpenseDto dto)
    {
        if (string.IsNullOrEmpty(UserId))
            return Unauthorized();

        var success = await _expenseService.UpdateAsync(
            id,
            dto,
            UserId);

        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE : api/Expense/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (string.IsNullOrEmpty(UserId))
            return Unauthorized();

        var success = await _expenseService.DeleteAsync(
            id,
            UserId);

        if (!success)
            return NotFound();

        return NoContent();
    }
}