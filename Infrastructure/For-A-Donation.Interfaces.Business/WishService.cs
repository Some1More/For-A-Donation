using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using For_A_Donation.Services.Interfaces.Models;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Business;

public class WishService : IWishService
{
    private readonly IUnitOfWork _db;

    public WishService(IUnitOfWork db)
    {
        _db = db;
    }

    public List<Wish> GetAll()
    {
        return _db.Wish.GetAll().ToList();
    }

    public List<Wish> GetByFilter(WishFilter model)
    {
        var wishes = _db.Wish.GetAll();

        if (model.Category != null)
        {
            wishes = wishes.Where(x => x.Category == model.Category);
        }

        if (model.UserId != null)
        {
            wishes = wishes.Where(x => x.UserId == model.UserId);
        }

        return wishes.ToList();
    }

    public Wish GetById(Guid id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(id));
        }

        var res = _db.Wish.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(id), "Wish with this Id was not founded");
        }

        return res;
    }

    public async Task<Wish> Create(Wish wish)
    {
        await _db.Wish.AddAsync(wish);
        return wish;
    }

    public async Task<Wish> Update(Wish wish)
    {
        if (string.IsNullOrEmpty(wish.Id.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(wish.Id));
        }

        var res = _db.Wish.GetById(wish.Id);

        if (res == null)
        {
            throw new NotFoundException(nameof(wish.Id), "Wish with this Id was not founded");
        }

        wish.UserId = res.UserId;
        await _db.Wish.UpdateAsync(wish);

        return wish;
    }

    public async Task Delete(Guid id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(id));
        }

        var res = _db.Wish.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(id), "Wish with this Id was not founded");
        }

        await _db.Wish.RemoveAsync(res);
    }
}
