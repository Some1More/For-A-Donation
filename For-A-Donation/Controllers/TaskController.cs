using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using For_A_Donation.Models.ViewModels;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskServicecs _taskService;
    private readonly IUserProgressService _userProgressService;
    private readonly IMapper _mapper;

    public TaskController(ITaskServicecs taskService, IUserProgressService userProgressService, IMapper mapper)
    {
        _taskService = taskService;
        _userProgressService = userProgressService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult< List<TaskViewModelResponse> > GetAll()
    {
        var res = _taskService.GetAll();
        var result = _mapper.Map<List<TaskViewModelResponse>>(res);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult< TaskViewModelResponse > GetById(int id)
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
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{name}")]
    public ActionResult< TaskViewModelResponse > GetByName(string name)
    {
        try
        {
            var res = _taskService.GetByName(name);
            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{numberCategory:int}")]
    public ActionResult<List< TaskViewModelResponse >> GetByCategory(int numberCategory)
    {
        var category = (CategoryOfTask)Enum.ToObject(typeof(CategoryOfTask), numberCategory);
        var res = _taskService.GetByCategory(category);
        var result = _mapper.Map<List<TaskViewModelResponse>>(res);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult< TaskViewModelResponse >> Create(TaskViewModelRequest model)
    {
        try
        {
            var task = _mapper.Map<Models.DataBase.Task>(model);
            var res = await _taskService.Create(task);
            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Created(new Uri(""), result);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult< TaskViewModelResponse >> Update(int id, TaskViewModelRequest model)
    {
        try
        {
            var task = _mapper.Map<Models.DataBase.Task>(model);
            task.Id = id;

            var res = await _taskService.Update(task);
            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:int},{userId:int}")]
    public async Task<ActionResult> FinishedTask(int id, int userId)
    {
        try
        {
            var task = await _taskService.FinishedTask(id);

            UserProgress progress = new() { UserId = userId, Points = task.Points, CategoryOfTask = task.CategoryOfTask };
            await _userProgressService.Update(progress);

            var result = _mapper.Map<TaskViewModelResponse>(task);

            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _taskService.Delete(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
