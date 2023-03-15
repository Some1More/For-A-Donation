using For_A_Donation.Domain.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Context _context;

    private bool _isDisposed = false;

    public IFamilyRepository Family { get; private set;  }
    public IProgressRepository Progress { get; private set; }
    public IPurposeRepository Purpose { get; private set; }
    public IRewardRepository Reward { get; private set; }
    public ITaskRepository Task { get; private set; }
    public IUserRepository User { get; private set; }
    public IUserProgressRepository UserProgress { get; private set; }
    public IWishRepository Wish { get; private set; }

    public UnitOfWork(Context context)
    {
        _context = context;
        Family = new FamilyRepository(context);
        Progress = new ProgressRepository(context);
        Purpose = new PurposeRepository(context);
        Reward = new RewardRepository(context);
        Task = new TaskRepository(context);
        User = new UserRepository(context);
        UserProgress = new UserProgressRepository(context);
        Wish = new WishRepository(context);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        // Освобождаем все ресурсы
        Dispose(true);

        // Подавляем финализацию
        GC.SuppressFinalize(this);
    }

    public virtual void Dispose(bool disposing)
    {
        if (!_isDisposed && disposing)
        {
            // Освобождаем управляемые ресурсы
        }

        // Освобождаем неуправляемые ресурсы
        _context.Dispose();
        _isDisposed = true;
    }
}
