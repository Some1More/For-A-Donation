using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels;

public class RewardViewModelRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public CategoryOfReward CategoryOfReward { get; set; }

    public List<ProgressViewModel> Progress { get; set; } = new();
}
