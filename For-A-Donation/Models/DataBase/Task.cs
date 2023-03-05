using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

public class Task : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int ExecutorId { get; set; }

    public User Executor { get; set; }

    public int CustomerId { get; set; }

    public User Customer { get; set; }

    public int Points { get; set; }

    public CategoryOfTask CategoryOfTask { get; set; }

    public DateTime DateTimeStart { get; } = DateTime.Now;

    public DateTime DateTimeFinish { get; set; }

    public bool IsFinished { get; set; } = false;
}
