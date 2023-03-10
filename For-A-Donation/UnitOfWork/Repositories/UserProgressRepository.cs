using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class UserProgressRepository : GenericRepository<UserProgress>
{
    public UserProgressRepository(Context context) : base(context)
    { }
}
