using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using System.Security.Cryptography;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Business;

public class UserService : IUserService
{
    private readonly IUnitOfWork _db;

    public UserService(IUnitOfWork db)
    {
        _db = db;
    }

    public User GetById(Guid id)
    {
        var res = _db.User.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(User), "Account with this id was not founded");
        }

        return res;
    }

    public User Get(string login, string password)
    {
        password = HashPassword(password);

        var res = _db.User.GetAll().SingleOrDefault(x => x.PhoneNumber == login && x.Password == password);

        if (res == null)
        {
            throw new NotFoundException(nameof(User), "User was not founded");
        }

        return res;
    }

    public async Task<User> Registration(User user)
    {
        if (!CheckExistByLogin(user.PhoneNumber))
        {
            throw new ObjectNotUniqueException("User with this phone number already exists");
        }

        else if (_db.Family.GetById(user.FamilyId.Value) == null)
        {
            throw new NotFoundException(nameof(user.FamilyId), "Family with this Id was not founded");
        }

        user.Password = HashPassword(user.Password);

        await _db.User.AddAsync(user);

        return user;
    }

    public async Task<User> Update(User user)
    {
        if (!CheckExistByLogin(user.PhoneNumber))
        {
            throw new ObjectNotUniqueException(nameof(user), "User with this phoneNumber already exists");
        }

        var res = _db.User.GetById(user.Id);

        if (res == null)
        {
            throw new NotFoundException(nameof(user), "Account with this id was not founded!");
        }

        else if (user.Password != res.Password)
        {
            throw new ForbiddenExeption("Updating a non-own account");
        }

        else if (_db.Family.GetById(user.FamilyId.Value) == null)
        {
            throw new NotFoundException(nameof(user.FamilyId), "Family with this Id was not founded");
        }

        await _db.User.UpdateAsync(user);

        return user;
    }

    public async Task Delete(Guid id)
    {
        var user = _db.User.GetById(id);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), "Account with this id was not founded");
        }

        await _db.User.RemoveAsync(user);
    }

    private bool CheckExistByLogin(string login)
    {
        var res = _db.User.GetAll().SingleOrDefault(x => x.PhoneNumber == login);

        if (res != null)
            return false;

        return true;
    }

    private static string HashPassword(string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password), "Password is required");
        }

        using SHA256 sha = SHA256.Create();
        byte[] passwordBytes = Encoding.Default.GetBytes(password);

        return sha.ComputeHash(passwordBytes).ToString();
    }
}
