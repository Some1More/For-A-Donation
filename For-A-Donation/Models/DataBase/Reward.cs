using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

public class Reward : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public CategoryOfReward CategoryOfReward { get; set; }

    public List<Progress> Progresses { get; set; } = new();

    public bool IsGotten { get; set; }
}