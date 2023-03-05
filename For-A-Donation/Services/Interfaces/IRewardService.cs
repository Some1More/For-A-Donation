using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using For_A_Donation.Exceptions;
using Task = System.Threading.Tasks.Task;

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
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    Reward GetById(int id);

    /// <summary>
    /// Получение награды по имени
    /// </summary>
    /// <param name="name"> Имя награды </param>
    /// <returns> Награда </returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    Reward GetByName(string name);
    
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
    /// <exception cref="ObjectNotUniqueException"></exception>
    Task<Reward> Create(Reward reward);


    /// <summary>
    /// Изменение награды
    /// </summary>
    /// <param name="reward"> Награда на изменение </param>
    /// <returns> Изменённая награда </returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ObjectNotUniqueException"></exception>
    Task<Reward> Update(Reward reward);

    /// <summary>
    /// Пользователь получил награду
    /// </summary>
    /// <param name="id"> Id награды </param>
    /// <returns> Награда </returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    Task<Reward> GottenReward(int id);

    /// <summary>
    /// Удаление награды
    /// </summary>
    /// <param name="id"> Id награды </param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(int id);
}
