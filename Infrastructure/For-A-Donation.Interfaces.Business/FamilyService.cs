using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Business;

public class FamilyService : IFamilyService
{

    private readonly IUnitOfWork _db;

    public FamilyService(IUnitOfWork context)
    {
        _db = context;
    }

    public Family GetById(Guid id)
    {
        var res = _db.Family.Include(x => x.Members).SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Family), "Family with this id not founded");
        }

        return res;
    }

    public async Task<Family> Create()
    {
        Family family = new();
        await _db.Family.AddAsync(family);

        return family;
    }

    public async Task Delete(Guid id)
    {
        var res = _db.Family.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Family), "Family with this id not founded");
        }

        await _db.Family.RemoveAsync(res);
    }
}
