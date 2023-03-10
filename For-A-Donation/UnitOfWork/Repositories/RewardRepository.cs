using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class RewardRepository : GenericRepository<Reward>
{
    public RewardRepository(Context context) : base(context)
    { }
}
