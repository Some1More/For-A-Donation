using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class ProgressService : IProgressService
{
    private readonly IUnitOfWork _db;

    public ProgressService(IUnitOfWork db)
    {
        _db = db;
    }

    public List<Progress> GetByRewardId(Guid rewardId)
    {
        return _db.Progress.GetAll().Where(x => x.RewardId == rewardId).ToList();
    }

    /*public async Task<List<Progress>> Create(List<Progress> progress)
    {
        await _db.Progress.AddRangeAsync(progress);
        await _db.SaveChangesAsync();
        
        return progress;
    }*/

    /*public async Task<List<Progress>> Update(List<Progress> progress)
    {   
        _db.Progress.UpdateRange(progress);
        await _db.SaveChangesAsync();

        return progress;
    }*/

    public async Task Delete(Guid id)
    {
        var res = _db.Progress.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Progress), "Progress with this id was not founded");
        }

        await _db.Progress.RemoveAsync(res);
    }
}
