using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels.Purpose;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PurposeController : ControllerBase
{
    private readonly IPurposeService _purposeService;
    private readonly IMapper _mapper;

    public PurposeController(IMapper mapper, IPurposeService purposeService)
    {
        _mapper = mapper;
        _purposeService = purposeService;
    }

    [HttpGet]
    [Route("{userId:int}")]
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
    }


    [HttpPost]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult< PurposeViewModelResponse >> Create(PurposeViewModelRequest model)
    {
        var purpose = _mapper.Map<Purpose>(model);
        var res = await _purposeService.Create(purpose);
        var result = _mapper.Map<PurposeViewModelResponse>(res);

        return Created(new Uri($"https://localhost:7006/api/Purpose/GetByUserid/{res.UserId}"), result);
    }


    [HttpDelete]
    [Route("{Id:int}")]
    [Authorize(new string[] {"Son", "Daughter" })]
    public async Task<ActionResult> Delete(Guid Id)
    {
        try
        {
            await _purposeService.Delete(Id);
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
