using For_A_Donation.Models.ViewModels.Reward;

namespace For_A_Donation.Models.ViewModels.Purpose;

public class PurposeViewModelResponse
{
    public Guid Id { get; set; }

    public RewardViewModelResponse Reward { get; set; }

    public bool IsFinished { get; set; }
}
