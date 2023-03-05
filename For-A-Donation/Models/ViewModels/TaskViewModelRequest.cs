using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels;

public class TaskViewModelRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Value is out of range")]
    public int ExecutorId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Value is out of range")]
    public int CustomerId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value is out of range")]
    public int Points { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }

    public DateTime DateTimeFinish { get; set; }
}
