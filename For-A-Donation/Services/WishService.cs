﻿using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class WishService : IWishService
{
    private readonly Context _db;

    public WishService(Context db)
    {
        _db = db;
    }

    public List<Wish> GetAll()
    {
        return _db.Wishes.AsNoTracking().ToList();
    }

    public List<Wish> GetByFilter(CategoryOfReward? category, Guid? userId)
    {
        var wishes = _db.Wishes.AsNoTracking().ToList();

        if (category != null)
        {
            wishes = wishes.Where(x => x.Category == category).ToList();
        }

        if (userId != null)
        {
            wishes = wishes.Where(x => x.UserId == userId).ToList();
        }

        return wishes;
    }

    public Wish GetById(Guid id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(id));
        }

        var res = _db.Wishes.AsNoTracking().SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(id), "Wish with this Id was not founded");
        }

        return res;
    }

    public async Task<Wish> Create(Wish wish)
    {
        await _db.Wishes.AddAsync(wish);
        await _db.SaveChangesAsync();

        return wish;
    }

    public async Task<Wish> Update(Wish wish)
    {
        if (string.IsNullOrEmpty(wish.Id.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(wish.Id));
        }

        var res = _db.Wishes.AsNoTracking().SingleOrDefault(x => x.Id == wish.Id);

        if (res == null)
        {
            throw new NotFoundException(nameof(wish.Id), "Wish with this Id was not founded");
        }

        _db.Wishes.Update(wish);
        await _db.SaveChangesAsync();

        return wish;
    }

    public async Task Delete(Guid id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
        {
            throw new ArgumentException("Id is required", nameof(id));
        }

        var res = _db.Wishes.AsNoTracking().SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(id), "Wish with this Id was not founded");
        }

        _db.Wishes.Remove(res);
        await _db.SaveChangesAsync();
    }
}