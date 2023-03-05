using For_A_Donation.Models.Enums;
using Task = For_A_Donation.Models.DataBase.Task;

namespace For_A_Donation.Services.Interfaces;

public interface ITaskServicecs
{
    List<Task> GetAll();

    Task GetById(int id);

    Task GetByName(string name);

    List<Task> GetByCategory(CategoryOfTask category);

    Task<Task> Create(Task task);

    Task<Task> Update(Task task);

    System.Threading.Tasks.Task Delete(int id);
}
