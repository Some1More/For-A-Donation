using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;

public class PurposeRepository : GenericRepository<Purpose>, IPurposeRepository
{
    public PurposeRepository(Context context) : base(context)
    {}
}
