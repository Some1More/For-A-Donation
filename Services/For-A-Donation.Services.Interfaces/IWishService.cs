using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Services.Interfaces.Exceptions;
using For_A_Donation.Services.Interfaces.Models;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IWishService
{
    /// <summary>
    /// Получения списка всех желаний
    /// </summary>
    /// <returns> Список желаний </returns>
    List<Wish> GetAll();

    /// <summary>
    /// Получение желания по Id
    /// </summary>
    /// <param name="id"> Id желания </param>
    /// <returns> Желания </returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    Wish GetById(Guid id);

    /// <summary>
    /// Получение отфильтрованных желаний
    /// </summary>
    /// <param name="model"> Фильтр </param>
    /// <returns> Список отфильтрованных желаний </returns>
    List<Wish> GetByFilter(WishFilter model);

    /// <summary>
    /// Созданиие нового желания
    /// </summary>
    /// <param name="wish"> Новое желание </param>
    /// <returns> Созданное желание </returns>
    Task<Wish> Create(Wish wish);

    /// <summary>
    /// Изменение желания
    /// </summary>
    /// <param name="wish"> Желание на изменение </param>
    /// <returns> Изменённое желание </returns>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    Task<Wish> Update(Wish wish);

    /// <summary>
    /// Удаление желания
    /// </summary>
    /// <param name="id"> Id желания </param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    Task Delete(Guid id);
}
