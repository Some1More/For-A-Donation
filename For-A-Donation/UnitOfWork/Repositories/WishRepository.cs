using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class WishRepository : GenericRepository<Wish>
{
    public WishRepository(Context context) : base(context)
    { }
}
