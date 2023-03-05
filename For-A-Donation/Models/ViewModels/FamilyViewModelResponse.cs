namespace For_A_Donation.Models.ViewModels;

public class FamilyViewModelResponse
{
    public int Id { get; set; }

    public List<UserListViewModelResponse> Members { get; set; } = new();
}
