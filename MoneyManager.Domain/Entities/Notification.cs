using MoneyManager.Domain.Enums;

namespace MoneyManager.Domain.Entities;

public class Notification
{
public int Id { get; set; }


public string Title { get; set; } = string.Empty;

public string Message { get; set; } = string.Empty;

public NotificationType Type { get; set; }

public bool IsRead { get; set; } = false;

public DateTime? ReadAt { get; set; }

public DateTime CreatedAt { get; set; }

public DateTime UpdatedAt { get; set; }

public string UserId { get; set; } = string.Empty;

public ApplicationUser User { get; set; } = null!;


}
