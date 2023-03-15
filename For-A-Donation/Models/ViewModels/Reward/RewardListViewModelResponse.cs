using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Models.ViewModels.Reward;

public class RewardListViewModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public CategoryOfReward CategoryOfReward { get; set; }
}
