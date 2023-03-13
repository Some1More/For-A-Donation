using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.User;

public class UserViewModelAuthorization
{
    [Phone]
    [Required]
    public string? PhoneNumber { get; set; }

    [Required]
    public string? Password { get; set; }
}
