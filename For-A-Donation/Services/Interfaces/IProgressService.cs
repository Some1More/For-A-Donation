using For_A_Donation.Models.DataBase;
using For_A_Donation.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IProgressService
{
    /// <summary>
    /// Получение необходимого прогресса по награде
    /// </summary>
    /// <param name="rewardId"> Id награды </param>
    /// <returns> Необходимый прогресс по награде </returns>
    List<Progress> GetByRewardId(Guid rewardId);

    /// <summary>
    /// Создание списка прогресса по награде
    /// </summary>
    /// <param name="progress"> Прогресс по награде </param>
    /// <returns> Прогресс по награде </returns>
    //Task<List<Progress>> Create(List<Progress> progress);

    /// <summary>
    /// Изменение прогресса по награде
    /// </summary>
    /// <param name="progress"> Прогресс по награде </param>
    /// <returns> Прогресс по награде </returns>
    //Task<List<Progress>> Update(List<Progress> progress);

    /// <summary>
    /// Удаление прогресса одной категории по награде
    /// </summary>
    /// <param name="id"> Id прогресса одной категории </param>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(Guid id);
}
