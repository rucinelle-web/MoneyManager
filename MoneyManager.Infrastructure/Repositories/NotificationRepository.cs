using Microsoft.EntityFrameworkCore;
using MoneyManager.Application.Interfaces;
using MoneyManager.Domain.Entities;
using MoneyManager.Infrastructure.Data;

namespace MoneyManager.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
private readonly ApplicationDbContext _context;

public NotificationRepository(ApplicationDbContext context)
{
    _context = context;
}

public async Task<IEnumerable<Notification>> GetAllAsync()
{
    return await _context.Notifications.ToListAsync();
}

public async Task<Notification?> GetByIdAsync(int id)
{
    return await _context.Notifications
        .FirstOrDefaultAsync(n => n.Id == id);
}

public async Task AddAsync(Notification notification)
{
    await _context.Notifications.AddAsync(notification);
    await _context.SaveChangesAsync();
}

public async Task UpdateAsync(Notification notification)
{
    _context.Notifications.Update(notification);
    await _context.SaveChangesAsync();
}

public async Task DeleteAsync(Notification notification)
{
    _context.Notifications.Remove(notification);
    await _context.SaveChangesAsync();
}

public async Task<IEnumerable<Notification>> GetByUserIdAsync(string userId)
{
    return await _context.Notifications
        .Where(n => n.UserId == userId)
        .OrderByDescending(n => n.CreatedAt)
        .ToListAsync();
}

}
