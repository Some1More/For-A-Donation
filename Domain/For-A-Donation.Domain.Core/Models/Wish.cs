using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Domain.Core.Models;

public class Wish
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public CategoryOfReward Category { get; set; }
}
