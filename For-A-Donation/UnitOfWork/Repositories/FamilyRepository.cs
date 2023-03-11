using For_A_Donation.Models.DataBase;

namespace For_A_Donation.UnitOfWork.Repositories;

public class FamilyRepository : GenericRepository<Family>
{
    public FamilyRepository(Context context) : base(context)
    { }
}
