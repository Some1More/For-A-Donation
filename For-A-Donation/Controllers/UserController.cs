using AnimalAPI.Helpers.Attributes;
using AutoMapper;
using For_A_Donation.Domain.Core.Enums;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.ViewModels.User;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[Controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IUserProgressService _userProgressService;
    private readonly IFamilyService _familyService;
    private readonly IMapper _mapper;

    public UserController(IUnitOfWork unitOfWork, IUserService userService, IUserProgressService userProgressService,
                            IMapper mapper, IFamilyService familyService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _userProgressService = userProgressService;
        _mapper = mapper;
        _familyService = familyService;
    }

    [HttpGet("{id:Guid}")]
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
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost]
    public ActionResult< UserViewModelResponse > Authorization(UserViewModelAuthorization model)
    {
        try
        {
            var user = _userService.Get(model.PhoneNumber, model.Password);
            var progress = _userProgressService.GetByUserId(user.Id);
            user.Progress = progress;

            var result = _mapper.Map<UserViewModelResponse>(user);

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

    [HttpPost]
    [Registration]
    public async Task<ActionResult< UserViewModelResponse >> Registration(UserViewModelRegistration model)
    {
        try
        {
            var user = _mapper.Map<User>(model);
            var family = new Family();

            if (string.IsNullOrEmpty(user.FamilyId.ToString()))
            {
                family = await _familyService.Create();
                user.FamilyId = family.Id;
                user.Family = family;
            }

            var res = await _userService.Registration(user);
            res.Progress = null;

            if (res.Role == Role.Son || res.Role == Role.Daughter)
            {
                var progress = await _userProgressService.Create(res.Id);
                res.Progress = progress;
            }

            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<UserViewModelResponse>(res);

            return Created(new Uri($"https://localhost:5165/User/GetById/{result.Id}"), result);
        }
        catch (ObjectNotUniqueException ex)
        {   
            return Conflict(ex.Message);
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

    [HttpPut("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< UserViewModelResponse >> Update(Guid id, UserViewModelRequest model)
    {
        try
        {
            var user = _mapper.Map<User>(model);
            user.Id = id;

            var res = await _userService.Update(user);
            await _unitOfWork.SaveChangesAsync();

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
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpDelete()]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public async Task<IActionResult> Delete()
    {
        try
        {
            var user = HttpContext.Items["User"] as User;

            await _userService.Delete(user.Id);
            await _unitOfWork.SaveChangesAsync();

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
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
