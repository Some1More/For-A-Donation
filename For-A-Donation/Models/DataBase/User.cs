using For_A_Donation.Models.Enums;

namespace For_A_Donation.Models.DataBase;

public class User : BaseEntity
{
    public string PhoneNumber { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }

    public string Name { get; set; }

    public Gender Gender { get; set; }

    public List<UserProgress> Progress { get; set; } = new();

    public Guid? FamilyId { get; set; } = null;

    public Family? Family { get; set; }

    public List<Task> CreatedTasks { get; set; } = new();

    public List<Task> CompleteTasks { get; set; } = new();
}
