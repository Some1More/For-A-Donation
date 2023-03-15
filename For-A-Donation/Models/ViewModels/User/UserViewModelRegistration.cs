using For_A_Donation.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.User;

public class UserViewModelRegistration
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Incorrect phone number")]
    public string? PhoneNumber { get; set; }


    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }


    [Required(ErrorMessage = "PasswordConfirm is required")]
    [Compare("Password", ErrorMessage = "Password mismatch")]
    public string? PasswordConfirm { get; set; }


    [Range(0, 1, ErrorMessage = "Value is out of range")]
    public Gender Gender { get; set; }


    [Range(0, 5, ErrorMessage = "Value is out of range")]
    public Role Role { get; set; }


    public Guid? FamilyId { get; set; }
}
