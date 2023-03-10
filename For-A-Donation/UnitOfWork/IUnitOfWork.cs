using For_A_Donation.UnitOfWork.Repositories;

namespace For_A_Donation.UnitOfWork;

public interface IUnitOfWork
{
    public FamilyRepository Family { get; }

    public ProgressRepository Progress { get; }

    public PurposeRepository Purpose { get; }

    public RewardRepository Reward { get; }

    public TaskRepository Task { get; }

    public UserRepository User { get; }

    public UserProgressRepository UserProgress { get; }

    public WishRepository Wish { get; }

    public Task SaveChanges();

    public void Dispose();
}
