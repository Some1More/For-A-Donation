using For_A_Donation.Models.DataBase;
using For_A_Donation.Exceptions;
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
    /// <param name="family"> Семья на создание </param>
    /// <returns> Созданная семья </returns>
    Task<Family> Create(Family family);

    /// <summary>
    /// Удаление семьи
    /// </summary>
    /// <param name="id"> Id семьи </param>
    /// <exception cref="NotFoundException"></exception>
    Task Delete(Guid id);
}
