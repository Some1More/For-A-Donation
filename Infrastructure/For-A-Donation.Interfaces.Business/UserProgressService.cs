using For_A_Donation.Domain.Core.Enums;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Business;

public class UserProgressService : IUserProgressService
{
    private readonly IUnitOfWork _db;

    public UserProgressService(IUnitOfWork db)
    {
        _db = db;
    }

    public List<UserProgress> GetByUserId(Guid userId)
    {
        var res = _db.UserProgress.GetAll().Where(x => x.UserId == userId).ToList();

        return res;
    }

    public async Task<List<UserProgress>> Create(Guid userId)
    {
        List<UserProgress> userProgress = new();

        foreach (var category in Enum.GetValues(typeof(CategoryOfTask)))
        {
            userProgress.Add(new UserProgress() { UserId = userId,
                                                    Points = 0,
                                                    CategoryOfTask = (CategoryOfTask)category});
        }

        await _db.UserProgress.AddRangeAsync(userProgress);

        return userProgress;
    }

    public async Task<UserProgress> Update(UserProgress userProgress)
    {
        await _db.UserProgress.UpdateAsync(userProgress);

        return userProgress;
    }

    public async Task Delete(List<UserProgress> userProgress)
    {
        await _db.UserProgress.RemoveRangeAsync(userProgress);
    }
}
