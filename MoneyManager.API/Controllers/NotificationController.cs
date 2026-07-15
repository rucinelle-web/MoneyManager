
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Application.Interfaces;
using System.Security.Claims;

namespace MoneyManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationController(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    private string? UserId =>
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    // GET : api/Notification
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var notifications =
            await _notificationRepository.GetByUserIdAsync(UserId);

        return Ok(notifications);
    }

    // GET : api/Notification/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var notification =
            await _notificationRepository.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound();
        }

        return Ok(notification);
    }

    // PUT : api/Notification/5/read
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var notification =
            await _notificationRepository.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound();
        }

        notification.IsRead = true;
        notification.ReadAt = DateTime.UtcNow;
        notification.UpdatedAt = DateTime.UtcNow;

        await _notificationRepository.UpdateAsync(notification);

        return NoContent();
    }

    // DELETE : api/Notification/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (string.IsNullOrEmpty(UserId))
        {
            return Unauthorized();
        }

        var notification =
            await _notificationRepository.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound();
        }

        await _notificationRepository.DeleteAsync(notification);

        return NoContent();
    }
}

