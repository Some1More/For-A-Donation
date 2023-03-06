using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.ViewModels;

public class UserViewModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }

    public List<UserProgressViewModelResponce> Progress { get; set; } = new();

    public Guid? FamilyId { get; set; }
}
