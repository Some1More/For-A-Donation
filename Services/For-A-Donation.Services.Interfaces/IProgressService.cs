using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Services.Interfaces.Exceptions;
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
    /// Удаление прогресса одной категории по награде
    /// </summary>
    /// <param name="id"> Id прогресса одной категории </param>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(Guid id);


    /// <summary>
    /// Удаление прогресса всех категорий по награде
    /// </summary>
    /// <param name="rewardId"> Id награды </param>
    Task DeleteListByUserId(Guid rewardId);
}
