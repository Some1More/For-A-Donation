using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

/// <summary>
/// Для создания награды
/// </summary>
public class Progress : BaseEntity
{
    public int RewardId { get; set; }

    public Reward Reward { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }

    public int PointsEnd { get; set;}
}
