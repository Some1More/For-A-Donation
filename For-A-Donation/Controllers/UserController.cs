using AnimalAPI.Helpers.Attributes;
using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[Controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserProgressService _userProgressService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IUserProgressService userProgressService, IMapper mapper)
    {
        _userService = userService;
        _userProgressService = userProgressService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< UserViewModelResponse > GetById(Guid id)
    {
        try
        {
            var user = _userService.GetById(id);
            var progress = _userProgressService.GetByUserId(id);
            user.Progress = progress;

            var result = _mapper.Map<UserViewModelResponse>(user);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Registration]
    [HttpPost]
    public async Task<ActionResult< UserViewModelResponse >> Registration(UserViewModelRegistration model)
    {
        try
        {
            var user = _mapper.Map<User>(model);

            var res = await _userService.Registration(user);
            var progress = await _userProgressService.Create(res.Id);
            res.Progress = progress;

            var result = _mapper.Map<UserViewModelResponse>(res);

            return Created(new Uri($"http://localhost:5165/User/GetById/{result.Id}"), result);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< UserViewModelResponse >> Update(Guid id, UserViewModelRequest model)
    {
        try
        {
            var user = _mapper.Map<User>(model);
            user.Id = id;

            var res = await _userService.Update(user);
            var result = _mapper.Map<UserViewModelResponse>(res);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ForbiddenExeption ex)
        {
            return Forbid(ex.Message);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _userService.Delete(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ForbiddenExeption ex)
        {
            return Forbid(ex.Message);
        }
    }
}
