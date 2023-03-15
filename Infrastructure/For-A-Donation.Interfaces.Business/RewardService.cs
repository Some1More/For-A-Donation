using For_A_Donation.Domain.Core.Enums;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Business;

public class RewardService : IRewardService
{
    private readonly IUnitOfWork _db;

    public RewardService(IUnitOfWork db)
    {
        _db = db;
    }

    public List<Reward> GetAll()
    {
        return _db.Reward.GetAll().ToList();
    }

    public Reward GetById(Guid id)
    {
        var res = _db.Reward.Include(x => x.Progress).SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this id was not founded");
        }

        return res;
    }

    public List<Reward> GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name is null or empty", nameof(name));
        }

        return _db.Reward.Include(x => x.Progress).Where(x => x.Name.Contains(name)).ToList();
    }

    public List<Reward> GetByCategory(CategoryOfReward category)
    {
        return _db.Reward.GetAll().Where(x => x.CategoryOfReward == category).ToList();
    }

    public async Task<Reward> Create(Reward reward)
    {
        await _db.Reward.AddAsync(reward);

        return reward;
    }

    public async Task<Reward> Update(Reward reward)
    {
        if (_db.Reward.GetById(reward.Id) == null)
        {
            throw new NotFoundException(nameof(reward), "Reward with this id was not founded");
        }

        await _db.Reward.UpdateAsync(reward);

        return reward;
    }

    public async Task<Reward> GottenReward(Guid id)
    {
        var reward = _db.Reward.GetById(id);

        if (reward == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this id was not founded");
        }

        reward.IsGotten = true;

        await _db.Reward.UpdateAsync(reward);

        return reward;
    }

    public async Task Delete(Guid id)
    {
        var res = _db.Reward.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this id was not founded");
        }

        await _db.Reward.RemoveAsync(res);
    }
}
