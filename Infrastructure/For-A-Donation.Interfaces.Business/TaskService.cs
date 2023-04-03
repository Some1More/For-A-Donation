using For_A_Donation.Domain.Core.Enums;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using For_A_Donation.Services.Interfaces.Models;
using System.Security;
using Task = For_A_Donation.Domain.Core.Models.Task;

namespace For_A_Donation.Services.Business;

public class TaskService : ITaskServicecs
{
    private readonly IUnitOfWork _db;

    public TaskService(IUnitOfWork db)
    {
        _db = db;
    }

    public List<Task> GetAllFamilyTask(Guid familyId)
    {
        var family = _db.Family.GetById(familyId);

        if (family == null)
        {
            throw new SecurityException();
        }

        return _db.Task.GetAll().Where(x => x.FamilyId == familyId).ToList();
    }

    public Task GetById(Guid id)
    {
        var res = _db.Task.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this id was not founded");
        }

        return res;
    }

    public List<Task> GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name is null or empty", nameof(name));
        }

        return _db.Task.GetAll().Where(x => x.Name.Contains(name)).ToList();
    }

    public List<Task> GetByFilter(TaskFilter model)
    {
        var tasks = _db.Task.GetAll();

        if (model.ExecutorId != null)
        {
            tasks = tasks.Where(x => x.ExecutorId == model.ExecutorId);
        }

        if (model.CustomerId != null)
        {
            tasks = tasks.Where(x => x.CustomerId == model.CustomerId);
        }

        if (model.Category != null)
        {
            tasks = tasks.Where(x => x.CategoryOfTask == model.Category);
        }

        return tasks.ToList();
    }

    public async Task<Task> Create(Task task)
    {
        ValidateForCreateTask(task.ExecutorId, task.CustomerId);

        await _db.Task.AddAsync(task);
        return task;
    }

    public async Task<Task> Update(Task task)
    {
        ValidateForCreateTask(task.ExecutorId, task.CustomerId);

        if (_db.Task.GetById(task.Id) == null)
        {
            throw new NotFoundException(nameof(task), "Task with this id was not founded");
        }

        await _db.Task.UpdateAsync(task);

        return task;
    }

    public async Task<Task> IsFinishedTask(Guid id)
    {
        var task = _db.Task.GetById(id);

        if (task == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this id was not founded");
        }

        task.IsFinished = true;
        task.IsPerformed = false;

        await _db.Task.UpdateAsync(task);

        return task;
    }

    public async Task<Task> IsNotFinishedTask(Guid id)
    {
        var task = _db.Task.GetById(id);

        if (task == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this id was not founded");
        }

        task.IsPerformed = false;

        await _db.Task.UpdateAsync(task);

        return task;
    }

    public async System.Threading.Tasks.Task Delete(Guid id)
    {
        var res = _db.Task.GetById(id);

        if (res == null)
        {
            throw new NotFoundException(nameof(Task), "Task with this id was not founded");
        }

        await _db.Task.RemoveAsync(res);
    }

    private void ValidateForCreateTask(Guid executorId, Guid customerId)
    {
        if (executorId == new Guid())
        {
            throw new ArgumentException("Executor Id is required", nameof(executorId));
        }

        else if (customerId == new Guid())
        {
            throw new ArgumentException("Cunsumer Id is required", nameof(customerId));
        }

        else if (customerId == executorId)
        {
            throw new CreateTaskException("Customer Id = Executor Id");
        }

        var executor = _db.User.GetById(executorId);

        if (executor == null)
        {
            throw new NotFoundException(nameof(User), "User-Executor with this Id was not founded");
        }

        else if (executor.Role != Role.Son && executor.Role != Role.Daughter)
        {
            throw new CreateTaskException(nameof(customerId), "Executor cannot be of legal age");
        }

        var customer = _db.User.GetById(customerId);

        if (customer == null)
        {
            throw new NotFoundException(nameof(User), "User_Customer with this Id was not founded");
        }

        else if (customer.Role == Role.Son || customer.Role == Role.Daughter)
        {
            throw new CreateTaskException(nameof(customerId), "Customer cannot be a child");
        }

        else if (executor.FamilyId != customer.FamilyId)
        {
            throw new CreateTaskException("Executor and customer from different families");
        }
    }
}
