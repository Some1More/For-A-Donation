using For_A_Donation.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.Wish;

public class WishViewModelRequest
{
    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Range(0, 5, ErrorMessage = "Value is out of range")]
    public CategoryOfReward Category { get; set; }
}
