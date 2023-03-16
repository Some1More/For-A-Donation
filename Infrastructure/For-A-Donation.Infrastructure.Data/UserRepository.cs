using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(Context context) : base(context)
    { }
}
