using For_A_Donation.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.Wish;

public class WishFilterViewModel
{
    public Guid? UserId { get; set; }

    [Range(0, 4)]
    public CategoryOfReward? Category { get; set; }
}
