using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class FamilyService : IFamilyService
{

    private readonly IUnitOfWork _db;

    public FamilyService(IUnitOfWork context)
    {
        _db = context;
    }

    public Family GetById(Guid id)
    {
        var res = _db.Family.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Family), "Family with this id not founded");
        }

        return res;
    }

    public async Task<Family> Create(Family family)
    {
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
