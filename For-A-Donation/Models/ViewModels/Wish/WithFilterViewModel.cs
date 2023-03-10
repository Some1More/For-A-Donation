using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.Wish;

public class WithFilterViewModel
{
    public Guid? UserId { get; set; }

    [Range(0, 4)]
    public CategoryOfReward? Category { get; set; }
}
