using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class ProgressRepository : GenericRepository<Progress>
{
    public ProgressRepository(Context context) : base(context)
    {}
}
