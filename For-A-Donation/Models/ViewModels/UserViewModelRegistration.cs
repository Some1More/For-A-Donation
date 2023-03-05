using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels;

public class UserViewModelRegistration
{
    [Required(ErrorMessage = "FirstName is null")]
    public string? Name { get; set; }


    [Required(ErrorMessage = "Email is null")]
    [Phone(ErrorMessage = "Incorrect phone number")]
    public string? PhoneNumber { get; set; }


    [Required(ErrorMessage = "Password is null")]
    public string? Password { get; set; }


    [Required(ErrorMessage = "PasswordConfirm is null")]
    [Compare("Password", ErrorMessage = "Password mismatch")]
    public string? PasswordConfirm { get; set; }

    public Gender Gender { get; set; }

    public Role Role { get; set; }

    [Range(1, 9999999999999999999, ErrorMessage = "Value is out of range")]
    public int? FamilyId { get; set; }
}
