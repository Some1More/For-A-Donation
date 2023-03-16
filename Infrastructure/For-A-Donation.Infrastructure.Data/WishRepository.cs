using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;

namespace For_A_Donation.Infrastructure.Data;

public class WishRepository : GenericRepository<Wish>, IWishRepository
{
    public WishRepository(Context context) : base(context)
    { }
}
