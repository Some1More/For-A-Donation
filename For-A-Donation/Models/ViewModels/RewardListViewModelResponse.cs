using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.ViewModels;

public class RewardListViewModelResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public CategoryOfReward CategoryOfReward { get; set; }
}
