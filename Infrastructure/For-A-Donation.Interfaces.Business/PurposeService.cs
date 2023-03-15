using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Business;

public class PurposeService : IPurposeService
{
    private readonly IUnitOfWork _db;

    public PurposeService(IUnitOfWork db)
    {
        _db = db;
    }

    public Purpose? GetByUserId(Guid userId)
    {
        if (string.IsNullOrEmpty(userId.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(userId));
        }

        var res = _db.Purpose.Include(x => x.Reward).SingleOrDefault(x => x.UserId == userId);

        return res;
    }

    public async Task<Purpose> Create(Purpose purpose)
    {
        var reward = _db.Reward.GetById(purpose.RewardId);

        if (reward == null)
        {
            throw new NotFoundException(nameof(purpose.RewardId), "Reward with this Id was not founded");
        }

        await _db.Purpose.AddAsync(purpose);

        return purpose;
    }

    public async Task Delete(Guid userId)
    {
        if (string.IsNullOrEmpty(userId.ToString()))
        {
            throw new ArgumentException("Id is required");
        }

        var res = _db.Purpose.GetAll().SingleOrDefault(x => x.UserId == userId);

        if (res == null)
        {
            throw new NotFoundException(nameof(userId), "Purpose with Id was not foudnded");
        }

        await _db.Purpose.RemoveAsync(res);
    }
}
