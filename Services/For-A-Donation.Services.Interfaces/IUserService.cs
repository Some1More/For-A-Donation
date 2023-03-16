using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Получение аккаунта по логину и паролю
    /// </summary>
    /// <param name="login"> Логин </param>
    /// <param name="password"> Пароль </param>
    /// <returns> Аккаунт пользователя </returns>
    /// <exception cref="NotFoundException"></exception>
    User Get(string login, string password);

    /// <summary>
    /// Получение аккаунта по Id пользователя
    /// </summary>
    /// <param name="Id"> Id пользователя </param>
    /// <returns> Аккаунт пользователя </returns>
    /// <exception cref="NotFoundException"></exception>
    User GetById(Guid Id);

    /// <summary>
    /// Регистрация аккаунта
    /// </summary>
    /// <param name="user"> Данные пользователь </param>
    /// <returns> Аккаунт пользователя </returns>
    /// <exception cref="ObjectNotUniqueException"</exception>
    Task<User> Registration(User user);

    /// <summary>
    /// Обновление данных аккаунта
    /// </summary>
    /// <param name="user"> Данные пользователя </param>
    /// <returns> Аккаунт пользователя </returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ObjectNotUniqueException"</exception>
    Task<User> Update(User user);

    /// <summary>
    /// Удаление аккаунта
    /// </summary>
    /// <param name="Id"> Id пользователя </param>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(Guid Id);
}
