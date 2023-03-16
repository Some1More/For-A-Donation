using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Domain.Core.Models;

public class Reward : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public CategoryOfReward CategoryOfReward { get; set; }

    public List<Progress> Progress { get; set; } = new();

    public bool IsGotten { get; set; }
}