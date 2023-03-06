using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.ViewModels;

public class RewardViewModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public CategoryOfReward CategoryOfReward { get; set; }

    public List<ProgressViewModel> Progress { get; set; } = new();

    public bool IsGotten { get; set; }
}
