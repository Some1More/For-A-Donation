using For_A_Donation.Models.DataBase;
using For_A_Donation.UnitOfWork.Repositories;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly Context _context;

    public FamilyRepository Family { get; }
    public ProgressRepository Progress { get; }
    public PurposeRepository Purpose { get; }
    public RewardRepository Reward { get; }
    public TaskRepository Task { get; }
    public UserRepository User { get; }
    public UserProgressRepository UserProgress { get; }
    public WishRepository Wish { get; }

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


    private bool _isDisposed = false;

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
