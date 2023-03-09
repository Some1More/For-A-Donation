using For_A_Donation.Models.DataBase.Abstract;
using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

public class User : BaseEntity
{
    public string Name { get; set; }

    public Gender Gender { get; set; }

    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }

    public Guid? FamilyId { get; set; }

    public Family? Family { get; set; }

    public List<UserProgress>? Progress { get; set; } = new();
}
