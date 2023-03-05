using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels;

public class UserViewModelRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Incorrect phone number")]
    public string? PhoneNumber { get; set; }


    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }


    [Range(1, 9999999999999999999, ErrorMessage = "Value is out of range")]
    public int? FamilyId { get; set; }
}
