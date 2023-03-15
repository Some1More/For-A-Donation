using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;

public class FamilyRepository : GenericRepository<Family>, IFamilyRepository
{
    public FamilyRepository(Context context) : base(context)
    { }
}
