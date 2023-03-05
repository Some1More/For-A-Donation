using AnimalAPI.Exceptions;
using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services;

public class UserService : IUserService
{
    private readonly Context _db;

    public UserService(Context db)
    {
        _db = db;
    }

    public User GetById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id <= 0", nameof(id));

        var res = _db.Users.AsNoTracking().SingleOrDefault(x => x.Id == id);

        if (res == null)
            throw new NotFoundException(nameof(User), "Account with this id was not founded");

        return res;
    }

    public User Get(string login, string password)
    {
        var res = _db.Users.AsNoTracking().SingleOrDefault(x => x.PhoneNumber == login && x.Password == password);

        if (res == null)
            throw new NotFoundException(nameof(User), "User was not founded");

        return res;
    }

    public async Task<User> Registration(User user)
    {
        if (!CheckExistByLogin(user.PhoneNumber))
            throw new ObjectNotUniqueException("User with this phone number already exists");

        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();

        return user;
    }

    public async Task<User> Update(User user)
    {
        if (user.Id <= 0)
            throw new ArgumentException("Id <= 0", nameof(user.Id));

        if (!CheckExistByLogin(user.PhoneNumber))
            throw new ObjectNotUniqueException(nameof(user), "User with this phoneNumber already exists");

        var res = _db.Users.AsNoTracking().SingleOrDefault(x => x.Id == user.Id);

        if (res == null)
            throw new NotFoundException(nameof(user), "Account with this id was not founded!");

        if (user.Password != res.Password)
            throw new ForbiddenExeption("Updating a non-own account");

        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        return user;
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Id <= 0", nameof(id));

        var user = _db.Users.SingleOrDefault(x => x.Id == id);

        if (user == null)
            throw new NotFoundException(nameof(User), "Account with this id was not founded");

        // todo: удаление не своего аккаунта

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    private bool CheckExistByLogin(string login)
    {
        var res = _db.Users.SingleOrDefault(x => x.PhoneNumber == login);

        if (res != null)
            return false;

        return true;
    }
}
