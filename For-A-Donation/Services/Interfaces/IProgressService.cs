using For_A_Donation.Models.DataBase;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IProgressService
{
    List<Progress> GetByRewardId(int rewardId);

    Task<List<Progress>> Create(List<Progress> progress);

    Task<List<Progress>> Update(List<Progress> progress);

    Task Delete(int id);
}
