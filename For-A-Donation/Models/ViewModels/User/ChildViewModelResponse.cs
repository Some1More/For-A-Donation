namespace For_A_Donation.Models.ViewModels.User;

public class ChildViewModelResponse : UserViewModelResponse
{
    public List<UserProgressViewModelResponce> Progress { get; set; } = new();
}
