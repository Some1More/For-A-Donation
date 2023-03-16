using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IFamilyService
{
    /// <summary>
    /// Плучение семьи по Id
    /// </summary>
    /// <param name="id"> Id семьи </param>
    /// <returns> Семья </returns>
    /// <exception cref="NotFoundException"></exception>
    Family GetById(Guid id);

    /// <summary>
    /// Создание семьи
    /// </summary>
    /// <returns> Созданная семья </returns>
    Task<Family> Create();

    /// <summary>
    /// Удаление семьи
    /// </summary>
    /// <param name="id"> Id семьи </param>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(Guid id);
}
