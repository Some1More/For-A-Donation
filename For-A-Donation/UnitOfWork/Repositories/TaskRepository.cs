using For_A_Donation.Models.DataBase;
using Task = For_A_Donation.Models.DataBase.Task;

namespace For_A_Donation.UnitOfWork.Repositories;

public class TaskRepository : GenericRepository<Task>
{
    public TaskRepository(Context context) : base(context)
    { }
}
