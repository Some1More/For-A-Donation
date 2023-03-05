using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

/// <summary>
/// Общий прогресс каждого пользователя (копилка)
/// </summary>
public class UserProgress : BaseEntity
{
    public int UserId { get; set; }

    public User User { get; set; }

    public int Points { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }
}
