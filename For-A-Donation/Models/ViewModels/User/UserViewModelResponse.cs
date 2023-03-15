using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Models.ViewModels.User;

public class UserViewModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }

    public Guid? FamilyId { get; set; }

    public List<UserProgressViewModelResponce>? Progress { get; set; } = new();
}
