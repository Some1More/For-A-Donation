using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Services.Interfaces.Models;

public class TaskFilter
{
    public Guid? ExecutorId { get; set; }

    public Guid? CustomerId { get; set;}

    public CategoryOfTask? Category { get; set; }
}
