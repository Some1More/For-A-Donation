using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Models.ViewModels.User;

public class UserListViewModelResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }
}
