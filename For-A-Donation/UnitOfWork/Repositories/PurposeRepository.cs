using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class PurposeRepository : GenericRepository<Purpose>
{
    public PurposeRepository(Context context) : base(context)
    {}
}
