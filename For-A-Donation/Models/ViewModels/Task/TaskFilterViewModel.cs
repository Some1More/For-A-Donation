using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Models.ViewModels.Task;

public class TaskFilterViewModel
{
    public Guid? ExecutorId { get; set; }

    public Guid? CustomerId { get; set;}

    public CategoryOfTask? Category { get; set; }
}
