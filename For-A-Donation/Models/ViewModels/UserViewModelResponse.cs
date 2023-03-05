using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.ViewModels;

public class UserViewModelResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }

    public List<UserProgressViewModelResponce> Progress { get; set; } = new();

    public int FamilyId { get; set; }
}
