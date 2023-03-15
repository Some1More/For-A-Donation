using AutoMapper;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.ViewModels.Family;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
public class FamilyController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFamilyService _familyService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public FamilyController(IUnitOfWork unitOfWork, IFamilyService familyService,  IMapper mapper, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _familyService = familyService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet("{id:Guid}")]
    public ActionResult< FamilyViewModelResponse > GetById(Guid id)
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
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost("{userId:Guid}")]
    public async Task<ActionResult< FamilyViewModelResponse >> Create(Guid userId)
    {
        try
        {
            var family = await _familyService.Create();

            var user = _userService.GetById(userId);
            user.FamilyId = family.Id;
            await _userService.Update(user);

            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<FamilyViewModelResponse>(family);

            return Created(new Uri($"https://localhost:7006/api/Family/GetById/{result.Id}"), result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
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

    [HttpPut("{userId:Guid},{familyId:Guid}")]
    public async Task<ActionResult< FamilyViewModelResponse >> AddMember(Guid userId, Guid familyId)
    {
        try
        {
            var user = _userService.GetById(userId);
            user.FamilyId = familyId;

            await _userService.Update(user);
            await _unitOfWork.SaveChangesAsync();

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
        catch (ForbiddenExeption ex)
        {
            return Forbid(ex.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _familyService.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
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
