using For_A_Donation.Models.Enums;
using Task = For_A_Donation.Models.DataBase.Task;
using For_A_Donation.Exceptions;

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
    /// Получение списка задач по категории
    /// </summary>
    /// <param name="category"> Категория задач </param>
    /// <returns> Список задач определённой категории </returns>
    List<Task> GetByCategory(CategoryOfTask category);

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
