using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;

public class ProgressRepository : GenericRepository<Progress>, IProgressRepository
{
    public ProgressRepository(Context context) : base(context)
    {}
}
