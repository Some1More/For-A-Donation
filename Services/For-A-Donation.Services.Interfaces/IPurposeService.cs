using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Services.Interfaces.Exceptions;
using Task = System.Threading.Tasks.Task;

namespace For_A_Donation.Services.Interfaces;

public interface IPurposeService
{
    /// <summary>
    /// Получение цели по Id пользователя
    /// </summary>
    /// <param name="userId"> Id пользователя </param>
    /// <returns> Цель </returns>
    /// <exception cref="ArgumentException"></exception>
    Purpose? GetByUserId(Guid userId);

    /// <summary>
    /// Созданиие новой цели
    /// </summary>
    /// <param name="purpose"> Новая цель </param>
    /// <returns> Созданная цель </returns>
    Task<Purpose> Create(Purpose purpose);

    /// <summary>
    /// Удаление цели
    /// </summary>
    /// <param name="Id"> Id цели </param>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="ArgumentException"></exception>
    Task Delete(Guid Id);
}
