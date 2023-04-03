using For_A_Donation.Domain.Core.Enums;

namespace For_A_Donation.Domain.Core.Models;

public class Task : BaseEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid ExecutorId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid FamilyId { get; set; }

    public int Points { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }

    public DateTime DateTimeStart { get; } = DateTime.Now;

    public DateTime DateTimeFinish { get; set; }

    public bool IsFinished { get; set; } = false;

    public bool IsPerformed { get; set; } = true;
}
