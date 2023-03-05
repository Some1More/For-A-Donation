using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IRewardService
{
    List<Reward> GetAll();

    Reward GetById(int id);

    Reward GetByName(string name);

    List<Reward> GetByCategory(CategoryOfReward category);

    Task<Reward> Create(Reward reward);

    Task<Reward> Update(Reward reward);

    Task Delete(int id);
}
