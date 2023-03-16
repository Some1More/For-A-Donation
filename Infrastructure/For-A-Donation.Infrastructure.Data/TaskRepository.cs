using For_A_Donation.Domain.Interfaces;
using Task = For_A_Donation.Domain.Core.Models.Task;

namespace For_A_Donation.Infrastructure.Data;

public class TaskRepository : GenericRepository<Task>, ITaskRepository
{
    public TaskRepository(Context context) : base(context)
    { }
}
