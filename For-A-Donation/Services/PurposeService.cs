using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class PurposeService : IPurposeService
{
    private readonly Context _db;

    public PurposeService(Context db)
    {
        _db = db;
    }

    public Purpose? GetByUserId(Guid userId)
    {
        if (string.IsNullOrEmpty(userId.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(userId));
        }

        var res = _db.Purposes.AsNoTracking().Include(x => x.Reward).SingleOrDefault(x => x.UserId == userId);

        return res;
    }

    public async Task<Purpose> Create(Purpose purpose)
    {
        var reward = _db.Rewards.SingleOrDefault(x => x.Id == purpose.RewardId);

        if (reward == null)
        {
            throw new NotFoundException(nameof(purpose.RewardId), "Reward with this Id was not founded");
        }

        await _db.Purposes.AddAsync(purpose);
        await _db.SaveChangesAsync();

        return purpose;
    }

    public async Task Delete(Guid userId)
    {
        if (string.IsNullOrEmpty(userId.ToString()))
        {
            throw new ArgumentException("Id is required");
        }

        var res = _db.Purposes.AsNoTracking().SingleOrDefault(x => x.UserId == userId);

        if (res == null)
        {
            throw new NotFoundException(nameof(userId), "Purpose with Id was not foudnded");
        }

        _db.Purposes.Remove(res);
        await _db.SaveChangesAsync();
    }
}
