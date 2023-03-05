using For_A_Donation.Models.DataBase;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IUserProgressService
{
    /// <summary>
    /// Получение списка прогресса пользователя по каждой категории
    /// </summary>
    /// <param name="userId"> Id пользователя </param>
    /// <returns> Список прогресса по каждой категории </returns>
    List<UserProgress> GetByUserId(int userId);

    /// <summary>
    /// Создание путого прогресса пользователя по каждой категории при регистрации
    /// </summary>
    /// <param name="userProgress"> Прогресс пользователя по всем категориям </param>
    /// <returns> Пустой список прогресса по каждой категории </returns>
    Task<List<UserProgress>> Create(List<UserProgress> userProgress);


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
