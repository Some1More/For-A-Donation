using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FamilyController : ControllerBase
{
    private readonly IFamilyService _familyService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public FamilyController(IFamilyService familyService,  IMapper mapper, IUserService userService)
    {
        _familyService = familyService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult< FamilyViewModelResponse > GetById(int id)
    {
        try
        {
            var res = _familyService.GetById(id);
            var result = _mapper.Map<FamilyViewModelResponse>(res);

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

    [HttpPost]
    [Route("{userId:int}")]
    public async Task<ActionResult< FamilyViewModelResponse >> Create(int userId, FamilyViewModelRequest model)
    {
        try
        {
            var family = _mapper.Map<Family>(model);
            var res = await _familyService.Create(family);

            var user = _userService.GetById(userId);
            user.FamilyId = family.Id;
            await _userService.Update(user);

            var result = _mapper.Map<FamilyViewModelResponse>(res);

            return Created(new Uri(""), result);
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
        catch (ForbiddenExeption ex)
        {
            return Forbid(ex.Message);
        }
    }

    [HttpPut]
    [Route("{userId:int},{familyId:int}")]
    public async Task<ActionResult< FamilyViewModelResponse >> AddMember(int userId, int familyId)
    {
        try
        {
            var user = _userService.GetById(userId);
            user.FamilyId = familyId;
            await _userService.Update(user);

            var family = _familyService.GetById(familyId);
            var result = _mapper.Map<FamilyViewModelResponse>(family);

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
        catch (ForbiddenExeption ex)
        {
            return Forbid(ex.Message);
        }

    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _familyService.Delete(id);
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
