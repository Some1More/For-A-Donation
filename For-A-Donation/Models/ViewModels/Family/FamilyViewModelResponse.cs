using For_A_Donation.Models.ViewModels.User;

namespace For_A_Donation.Models.ViewModels.Family;

public class FamilyViewModelResponse
{
    public Guid Id { get; set; }

    public List<UserListViewModelResponse> Members { get; set; } = new();
}
