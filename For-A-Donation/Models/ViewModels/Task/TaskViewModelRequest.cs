using For_A_Donation.Domain.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels.Task;

public class TaskViewModelRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public Guid ExecutorId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value is out of range")]
    public int Points { get; set; }

    [Range(0, 4, ErrorMessage = "Value is out of range")]
    public CategoryOfTask CategoryOfTask { get; set; }

    public DateTime DateTimeFinish { get; set; }
}
