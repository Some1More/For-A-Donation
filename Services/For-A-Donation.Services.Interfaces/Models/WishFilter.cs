using For_A_Donation.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Services.Interfaces.Models;

public class WishFilter
{
    public Guid? UserId { get; set; }

    [Range(0, 4)]
    public CategoryOfReward? Category { get; set; }
}
