using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.Enums;
using For_A_Donation.Models.ViewModels;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<TaskViewModelResponse> > GetAll()
    {
        var res = _taskService.GetAll();
        var result = _mapper.Map<List<TaskViewModelResponse>>(res);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:Guid}")]
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
    }

    [HttpGet]
    [Route("{name}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
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
        try
        {
            var category = (CategoryOfTask)Enum.ToObject(typeof(CategoryOfTask), numberCategory);
            var res = _taskService.GetByCategory(category);
            var result = _mapper.Map<List<TaskViewModelResponse>>(res);

            return Ok(result);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< TaskViewModelResponse >> Create(TaskViewModelRequest model)
    {
        try
        {
            var task = _mapper.Map<Models.DataBase.Task>(model);
            var res = await _taskService.Create(task);
            var result = _mapper.Map<TaskViewModelResponse>(res);

            return Created(new Uri($"https://localhost:7006/api/Task/GetById/{result.Id}"), result);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< TaskViewModelResponse >> Update(Guid id, TaskViewModelRequest model)
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
    }

    [HttpPut]
    [Route("{id:Guid},{userId:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult> FinishedTask(Guid id, Guid userId)
    {
        try
        {
            var task = await _taskService.FinishedTask(id);

            var progress = _userProgressService.GetByUserId(userId).Single(x => x.CategoryOfTask == task.CategoryOfTask);
            progress.Points += task.Points;

            await _userProgressService.Update(progress);

            var result = _mapper.Map<TaskViewModelResponse>(task);

            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<IActionResult> Delete(Guid id)
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
    }
}
