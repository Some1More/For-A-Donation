using For_A_Donation.Models.DataBase;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IUserService
{
    User Get(string login, string password);

    User GetById(int Id);

    Task<User> Registration(User user);

    Task<User> Update(User user);

    Task Delete(int Id);
}
