using For_A_Donation.Domain.Core.Models;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IUserProgressService
{
    /// <summary>
    /// Получение списка прогресса пользователя по каждой категории
    /// </summary>
    /// <param name="userId"> Id пользователя </param>
    /// <returns> Список прогресса по каждой категории </returns>
    List<UserProgress> GetByUserId(Guid userId);

    /// <summary>
    /// Создание пустого прогресса пользователя по каждой категории при регистрации
    /// </summary>
    /// <param name="userId"> Прогресс пользователя по всем категориям </param>
    /// <returns> Пустой список прогресса по каждой категории </returns>
    Task<List<UserProgress>> Create(Guid userId);


    /// <summary>
    /// Обновление прогресса пользователя по определённой категории
    /// </summary>
    /// <param name="userProgress"> Прогресс пользователя по определённой категории </param>
    /// <returns> Прогресс пользователя по определённой категории </returns>
    Task<UserProgress> Update(UserProgress userProgress);


    /// <summary>
    /// Удаление прогресса пользователя при удаления аккаунта
    /// </summary>
    /// <param name="userProgresses"> Прогресс пользователя по всем категориям </param>
    Task Delete(List<UserProgress> userProgresses);
}
