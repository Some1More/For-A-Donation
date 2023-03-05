using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class UserProgressService : IUserProgressService
{
    private readonly Context _db;

    public UserProgressService(Context db)
    {
        _db = db;
    }

    public List<UserProgress> GetByUserId(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("userId <= 0", nameof(userId));
        }

        var res = _db.UserProgress.AsNoTracking().Where(x => x.UserId == userId).ToList();

        return res;
    }

    public async Task<List<UserProgress>> Create(List<UserProgress> userProgress)
    {
        await _db.UserProgress.AddRangeAsync(userProgress);
        await _db.SaveChangesAsync();

        return userProgress;
    }

    public async Task<UserProgress> Update(UserProgress userProgress)
    {
        _db.UserProgress.Update(userProgress);
        await _db.SaveChangesAsync();

        return userProgress;
    }

    public async Task Delete(List<UserProgress> userProgress)
    {
        _db.UserProgress.RemoveRange(userProgress);
        await _db.SaveChangesAsync();
    }
}
