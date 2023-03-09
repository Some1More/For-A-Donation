using For_A_Donation.Models.DataBase.Abstract;

namespace For_A_Donation.Models.DataBase;

public class Family : BaseEntity
{
    public List<User> Members { get; set; } = new();
}