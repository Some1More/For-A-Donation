using For_A_Donation.Domain.Core.Enums;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;
using For_A_Donation.Domain.Core.Models;

namespace For_A_Donation.Services.Interfaces;

public interface IRewardService
{
    /// <summary>
    /// Получение всех наград
    /// </summary>
    /// <returns> Список всех наград </returns>
    List<Reward> GetAll();

    /// <summary>
    /// Получение награды по Id
    /// </summary>
    /// <param name="id"> Id награды </param>
    /// <returns> Награда </returns>
    /// <exception cref="NotFoundException"></exception>
    Reward GetById(Guid id);

    /// <summary>
    /// Получение списка наград по части от имени
    /// </summary>
    /// <param name="name"> Часть от имени </param>
    /// <returns> Список наград по части от имени </returns>
    /// <exception cref="ArgumentException"> Name is null or empty </exception>
    List<Reward> GetByName(string name);

    /// <summary>
    /// Получение наград определённой категории
    /// </summary>
    /// <param name="category"> Категория </param>
    /// <returns> Список наград </returns>
    List<Reward> GetByCategory(CategoryOfReward category);

    /// <summary>
    /// Создание награды
    /// </summary>
    /// <param name="reward"> Награда на создание </param>
    /// <returns> Созданная награда </returns>
    Task<Reward> Create(Reward reward);


    /// <summary>
    /// Изменение награды
    /// </summary>
    /// <param name="reward"> Награда на изменение </param>
    /// <returns> Изменённая награда </returns>
    /// <exception cref="NotFoundException"></exception>
    Task<Reward> Update(Reward reward);

    /// <summary>
    /// Пользователь получил награду
    /// </summary>
    /// <param name="id"> Id награды </param>
    /// <returns> Награда </returns>
    /// <exception cref="NotFoundException"></exception>
    Task<Reward> GottenReward(Guid id);

    /// <summary>
    /// Удаление награды
    /// </summary>
    /// <param name="id"> Id награды </param>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(Guid id);
}
