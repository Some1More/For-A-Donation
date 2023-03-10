using For_A_Donation.Models.DataBase.Abstract;

namespace For_A_Donation.Models.DataBase;

public class Purpose : BaseEntity
{ 
    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Guid RewardId { get; set; }

    public Reward? Reward { get; set; }

    public bool IsFinished { get; set; } = false;
}