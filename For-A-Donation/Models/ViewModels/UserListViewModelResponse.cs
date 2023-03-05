using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.ViewModels;

public class UserListViewModelResponse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }
}
