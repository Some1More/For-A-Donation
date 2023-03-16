using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;

public class RewardRepository : GenericRepository<Reward>, IRewardRepository
{
    public RewardRepository(Context context) : base(context)
    { }
}
