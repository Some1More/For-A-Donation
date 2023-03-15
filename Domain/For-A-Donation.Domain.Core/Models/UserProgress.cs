using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Domain.Core.Models;

/// <summary>
/// Общий прогресс каждого пользователя (копилка)
/// </summary>
public class UserProgress : BaseEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; }

    public int Points { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }
}
