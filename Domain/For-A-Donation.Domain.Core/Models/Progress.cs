using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Domain.Core.Models;

/// <summary>
/// Для создания награды
/// </summary>
public class Progress : BaseEntity
{
    public Guid RewardId { get; set; }

    public Reward Reward { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }

    public int PointsEnd { get; set;}
}
