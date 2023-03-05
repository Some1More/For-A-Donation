using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class ProgressService : IProgressService
{
    private readonly Context _db;

    public ProgressService(Context db)
    {
        _db = db;
    }

    public List<Progress> GetByRewardId(int rewardId)
    {
        if (rewardId <= 0)
        {
            throw new ArgumentException("rewardId <= 0", nameof(rewardId));
        }

        return _db.Progress.AsNoTracking().Where(x => x.RewardId == rewardId).ToList();
    }

    public async Task<List<Progress>> Create(List<Progress> progress)
    {
        await _db.Progress.AddRangeAsync(progress);
        await _db.SaveChangesAsync();
        
        return progress;
    }

    public async Task<List<Progress>> Update(List<Progress> progress)
    {   
        _db.Progress.UpdateRange(progress);
        await _db.SaveChangesAsync();

        return progress;
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Progress.SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Progress), "Progress with this id was not founded");
        }

        _db.Progress.Remove(res);
        await _db.SaveChangesAsync();
    }
}
