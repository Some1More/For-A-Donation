﻿using For_A_Donation.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace For_A_Donation.Models.ViewModels;

public class TaskViewModelRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public Guid ExecutorId { get; set; }

    public Guid CustomerId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Value is out of range")]
    public int Points { get; set; }

    [Range(0, 4, ErrorMessage = "Value is out of range")]
    public CategoryOfTask CategoryOfTask { get; set; }

    public DateTime DateTimeFinish { get; set; }
}
