using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Models.ViewModels.User;

public class UserProgressViewModelResponce
{
    public Guid Id { get; set; }

    public int Points { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }
}
