namespace For_A_Donation.Domain.Interfaces;

public interface IUnitOfWork
{
    public IFamilyRepository Family { get; }

    public IProgressRepository Progress { get; }

    public IPurposeRepository Purpose { get; }

    public IRewardRepository Reward { get; }

    public ITaskRepository Task { get; }

    public IUserRepository User { get; }

    public IUserProgressRepository UserProgress { get; }

    public IWishRepository Wish { get; }

    public Task SaveChangesAsync();

    public void Dispose();
}
