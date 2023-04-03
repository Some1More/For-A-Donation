using AutoMapper;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.ViewModels.Task;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using For_A_Donation.Services.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskServicecs _taskService;
    private readonly IUserProgressService _userProgressService;
    private readonly IMapper _mapper;

    public TaskController(ITaskServicecs taskService, IUserProgressService userProgressService,
                                IUnitOfWork unitOfWork, IMapper mapper)
    {
        _taskService = taskService;
        _userProgressService = userProgressService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<TaskViewModelResponse> > GetAllFamilyTask()
    {
        try
        {
            var familyId = ((User)HttpContext.Items["User"]).FamilyId;
            var res = _taskService.GetAllFamilyTask(familyId.Value);
            var result = _mapper.Map<List<TaskViewModelResponse>>(res);

            return Ok(result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpGet("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< TaskViewModelResponse > GetById(Guid id)
    {
        try
        {
            var res = _taskService.GetById(id);
            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpGet("{name}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<TaskViewModelResponse> > GetByName(string name)
    {
        try
        {
            var res = _taskService.GetByName(name);
            var result = _mapper.Map<List<TaskViewModelResponse>>(res);

            return Ok(result);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<TaskViewModelResponse>> GetByFilter(TaskFilterViewModel model)
    {
        try
        {
            var filter = _mapper.Map<TaskFilter>(model);
            var res = _taskService.GetByFilter(filter);
            var result = _mapper.Map<List<TaskViewModelResponse>>(res);

            return Ok(result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< TaskViewModelResponse >> Create(TaskViewModelRequest model)
    {
        try
        {
            var user = HttpContext.Items["User"] as User;

            var task = _mapper.Map<Domain.Core.Models.Task>(model);
            task.CustomerId = user.Id;
            task.FamilyId = user.FamilyId.Value;

            var res = await _taskService.Create(task);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Created(new Uri($"https://localhost:7006/api/Task/GetById/{result.Id}"), result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CreateTaskException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPut("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< TaskViewModelResponse >> Update(Guid id, TaskViewModelRequest model)
    {
        try
        {
            var user = HttpContext.Items["User"] as User;

            var task = _mapper.Map<Domain.Core.Models.Task>(model);
            task.Id = id;
            task.CustomerId = user.Id;

            var res = await _taskService.Update(task);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPatch("{taskId:Guid},{userId:Guid}")]
    [Authorize(new string[] { "Son", "Daughter", "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< TaskViewModelResponse >> IsFinishedTask(Guid taskId, Guid userId)
    {
        try
        {
            var task = await _taskService.IsFinishedTask(taskId);

            var progress = _userProgressService.GetByUserId(userId).Single(x => x.CategoryOfTask == task.CategoryOfTask);
            progress.Points += task.Points;

            await _userProgressService.Update(progress);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<TaskViewModelResponse>(task);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPatch("{taskId:Guid},{userId:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< TaskViewModelResponse >> IsNotFinishedTask(Guid taskId, Guid userId)
    {
        try
        {
            var task = await _taskService.IsNotFinishedTask(taskId);

            var progress = _userProgressService.GetByUserId(userId).Single(x => x.CategoryOfTask == task.CategoryOfTask);
            progress.Points -= task.Points / 2;

            await _userProgressService.Update(progress);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<TaskViewModelResponse>(task);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _taskService.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
