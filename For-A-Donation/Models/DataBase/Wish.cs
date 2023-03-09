using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

public class Wish
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public CategoryOfReward Category { get; set; }
}
