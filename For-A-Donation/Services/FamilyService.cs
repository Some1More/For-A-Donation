using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class FamilyService : IFamilyService
{

    private readonly Context _db;

    public FamilyService(Context context)
    {
        _db = context;
    }

    public Family GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Families.AsNoTracking().FirstOrDefault(f => f.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Family), "Family with this id not founded");
        }

        return res;
    }

    public async Task<Family> Create(Family family)
    {
        await _db.AddAsync(family);
        await _db.SaveChangesAsync();

        return family;
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Families.FirstOrDefault(f => f.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Family), "Family with this id not founded");
        }

        _db.Families.Remove(res);
        await _db.SaveChangesAsync();
    }
}
