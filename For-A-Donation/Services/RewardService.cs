using For_A_Donation.Services.Interfaces;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using Microsoft.EntityFrameworkCore;
using For_A_Donation.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class RewardService : IRewardService
{
    private readonly Context _db;

    public RewardService(Context db)
    {
        _db = db;
    }

    public List<Reward> GetAll()
    {
        return _db.Rewards.AsNoTracking().ToList();
    }

    public Reward GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Rewards.Include(x => x.Progress).AsNoTracking().SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this id was not founded");
        }

        return res;
    }

    public Reward GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name is null or empty", nameof(name));
        }

        var res = _db.Rewards.Include(x => x.Progress).AsNoTracking().SingleOrDefault(x => x.Name == name);

        if (res == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this name was not founded");
        }

        return res;
    }

    public List<Reward> GetByCategory(CategoryOfReward category)
    {
        return _db.Rewards.AsNoTracking().Where(x => x.CategoryOfReward == category).ToList();
    }

    public async Task<Reward> Create(Reward reward)
    {
        var res = _db.Rewards.AsNoTracking().SingleOrDefault(x => x.Name == reward.Name);

        if (res != null)
        {
            throw new ObjectNotUniqueException(nameof(reward), "Reward with this name already exsits");
        }

        await _db.Rewards.AddAsync(reward);
        await _db.SaveChangesAsync();

        return reward;
    }

    public async Task<Reward> Update(Reward reward)
    {
        if (reward.Id <= 0)
        {
            throw new ArgumentException("reward Id <= 0", nameof(reward));
        }

        if (_db.Rewards.AsNoTracking().SingleOrDefault(x => x.Id == reward.Id) == null)
        {
            throw new NotFoundException(nameof(reward), "Reward with this id was not founded");
        }

        if (_db.Rewards.AsNoTracking().SingleOrDefault(x => x.Name == reward.Name) != null)
        {
            throw new ObjectNotUniqueException(nameof(reward), "Reward with this name already exists");
        }

        _db.Rewards.Update(reward);
        await _db.SaveChangesAsync();

        return reward;
    }

    public async Task<Reward> GottenReward(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id <= 0", nameof(id));
        }

        var reward = _db.Rewards.Find(id);

        if (reward == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this id was not founded");
        }

        reward.IsGotten = true;

        _db.Rewards.Update(reward);
        await _db.SaveChangesAsync();

        return reward;
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Rewards.SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Reward), "Reward with this id was not founded");
        }

        _db.Rewards.Remove(res);
        await _db.SaveChangesAsync();
    }
}
