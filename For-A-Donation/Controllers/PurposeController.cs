using AutoMapper;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.ViewModels.Purpose;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PurposeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPurposeService _purposeService;
    private readonly IMapper _mapper;

    public PurposeController(IMapper mapper, IPurposeService purposeService, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _purposeService = purposeService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{userId:int}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public ActionResult< PurposeViewModelResponse > GetByUserId(Guid userId)
    {
        try
        {
            var res = _purposeService.GetByUserId(userId);
            var result = _mapper.Map<PurposeViewModelResponse>(res);

            return Ok(result);
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


    [HttpPost]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult< PurposeViewModelResponse >> Create(PurposeViewModelRequest model)
    {
        try
        {
            var purpose = _mapper.Map<Purpose>(model);

            var res = await _purposeService.Create(purpose);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<PurposeViewModelResponse>(res);

            return Created(new Uri($"https://localhost:7006/api/Purpose/GetByUserid/{res.UserId}"), result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }


    [HttpDelete("{Id:int}")]
    [Authorize(new string[] {"Son", "Daughter" })]
    public async Task<ActionResult> Delete(Guid Id)
    {
        try
        {
            await _purposeService.Delete(Id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
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
}
