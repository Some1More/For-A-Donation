using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = For_A_Donation.Models.DataBase.Task;

namespace For_A_Donation.Services;

public class TaskService : ITaskServicecs
{
    private readonly Context _db;

    public TaskService(Context db)
    {
        _db = db;
    }

    public List<Task> GetAll()
    {
        return _db.Tasks.AsNoTracking().ToList();
    }

    public Task GetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Tasks.AsNoTracking().SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this id was not founded");
        }

        return res;
    }

    public Task GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name is null or empty", nameof(name));
        }

        var res = _db.Tasks.AsNoTracking().SingleOrDefault(x => x.Name == name);

        if (res == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this name was not founded");
        }

        return res;
    }

    public List<Task> GetByCategory(CategoryOfTask category)
    {
         return _db.Tasks.AsNoTracking().Where(x => x.CategoryOfTask == category).ToList();
    }

    public async Task<Task> Create(Task task)
    {
        var res = _db.Tasks.AsNoTracking().SingleOrDefault(x => x.Name == task.Name);

        if (res != null)
        {
            throw new ObjectNotUniqueException(nameof(task), "Task with this name already exsits");
        }

        await _db.Tasks.AddAsync(task);
        await _db.SaveChangesAsync();

        return task;
    }

    public async Task<Task> Update(Task task)
    {
        if (task.Id <= 0)
        {
            throw new ArgumentException("task Id <= 0", nameof(task));
        }

        if (_db.Tasks.AsNoTracking().SingleOrDefault(x => x.Id == task.Id) == null)
        {
            throw new NotFoundException(nameof(task), "Task with this id was not founded");
        }

        if (_db.Tasks.AsNoTracking().SingleOrDefault(x => x.Name == task.Name) != null)
        {
            throw new ObjectNotUniqueException(nameof(task), "Task with this name already exists");
        }

        _db.Tasks.Update(task);
        await _db.SaveChangesAsync();

        return task;
    }

    public async System.Threading.Tasks.Task Delete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("id <= 0", nameof(id));
        }

        var res = _db.Tasks.SingleOrDefault(x => x.Id == id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this id was not founded");
        }

        _db.Tasks.Remove(res);
        await _db.SaveChangesAsync();
    }
}
