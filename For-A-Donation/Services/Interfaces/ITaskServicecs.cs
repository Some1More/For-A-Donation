using For_A_Donation.Models.Enums;
using Task = For_A_Donation.Models.DataBase.Task;
using For_A_Donation.Exceptions;
using For_A_Donation.Models.ViewModels.Task;

namespace For_A_Donation.Services.Interfaces;

public interface ITaskServicecs
{
    /// <summary>
    /// Получения списка всех невыполненных задач
    /// </summary>
    /// <returns> Список невыполненных задач </returns>
    List<Task> GetAll();

    /// <summary>
    /// Получение задачи по Id
    /// </summary>
    /// <param name="id"> Id задачи </param>
    /// <returns> Задача </returns>
    /// <exception cref="NotFoundException"></exception>
    Task GetById(Guid id);

    /// <summary>
    /// Получение списка задач по части от имени
    /// </summary>
    /// <param name="name"> Часть от имени </param>
    /// <returns> Список задач по части от имени </returns>
    /// <exception cref="ArgumentException"> Name is null or empty </exception>
    List<Task> GetByName(string name);

    /// <summary>
    /// Получение отфильтрованного списка задач
    /// </summary>
    /// <param name="model"> Фильтр </param>
    /// <returns> Отфильтрованный список задач </returns>
    List<Task> GetByFilter(TaskFilterViewModel model);

    /// <summary>
    /// Созданиие новой задачи
    /// </summary>
    /// <param name="task"> Новая задача </param>
    /// <returns> Созданная задача </returns>
    Task<Task> Create(Task task);

    /// <summary>
    /// Изменение задачи
    /// </summary>
    /// <param name="task"> Задача на изменение </param>
    /// <returns> Изменённая задача </returns>
    /// <exception cref="NotFoundException"></exception>
    Task<Task> Update(Task task);

    /// <summary>
    /// Выполнение задачи
    /// </summary>
    /// <param name="id"> Id задачи </param>
    /// <returns> Завершённая задача </returns>
    /// <exception cref="NotFoundException"></exception>
    Task<Task> FinishedTask(Guid id);

    /// <summary>
    /// Удаление задач
    /// </summary>
    /// <param name="id"> Id задачи </param>
    /// <exception cref="NotFoundException"></exception>
    System.Threading.Tasks.Task Delete(Guid id);
}
