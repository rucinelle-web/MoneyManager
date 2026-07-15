using MoneyManager.Domain.Entities;

namespace MoneyManager.Application.Interfaces;

public interface INotificationRepository
{
Task<IEnumerable<Notification>> GetAllAsync();

Task<Notification?> GetByIdAsync(int id);

Task AddAsync(Notification notification);

Task UpdateAsync(Notification notification);

Task DeleteAsync(Notification notification);
Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);

}
