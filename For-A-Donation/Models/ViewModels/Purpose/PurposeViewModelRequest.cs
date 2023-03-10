using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.Purpose;

public class PurposeViewModelRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid RewardId { get; set; }
}
