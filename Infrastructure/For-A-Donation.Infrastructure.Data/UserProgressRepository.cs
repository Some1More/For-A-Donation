using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;

public class UserProgressRepository : GenericRepository<UserProgress>, IUserProgressRepository
{
    public UserProgressRepository(Context context) : base(context)
    { }
}
