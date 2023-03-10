using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(Context context) : base(context)
    { }

}
