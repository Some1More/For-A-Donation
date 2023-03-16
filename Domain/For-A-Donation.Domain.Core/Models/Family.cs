namespace For_A_Donation.Domain.Core.Models;

public class Family : BaseEntity
{
    public List<User> Members { get; set; } = new();
}