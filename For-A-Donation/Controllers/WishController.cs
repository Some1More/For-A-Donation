using AutoMapper;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.ViewModels.Wish;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using For_A_Donation.Services.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class WishController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWishService _wishService;
    private readonly IMapper _mapper;

    public WishController(IUnitOfWork unitOfWork, IWishService wishService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _wishService = wishService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult<List<WishViewModelResponse>> GetAll()
    {
        try
        {
            var res = _wishService.GetAll();
            var result = _mapper.Map<List<WishViewModelResponse>>(res);

            return Ok(result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpGet("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult<WishViewModelResponse> GetById(Guid id)
    {
        try
        {
            var res = _wishService.GetById(id);
            var result = _mapper.Map<WishViewModelResponse>(res);

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
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost]
    public ActionResult<List<WishViewModelResponse>> GetByFilter(WishFilterViewModel model)
    {
        try
        {
            var filter = _mapper.Map<WishFilter>(model);
            var res = _wishService.GetByFilter(filter);
            var result = _mapper.Map<List<WishViewModelResponse>>(res);

            return Ok(result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost("{userId:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult<WishViewModelResponse>> Create(Guid userId, WishViewModelRequest model)
    {
        try
        {
            var wish = _mapper.Map<Wish>(model);
            wish.UserId = userId;
            //TODO: передача Id не своего аккаунта

            var res = await _wishService.Create(wish);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<WishViewModelResponse>(res);

            return Created(new Uri($"https://localhost:7006/api/Wish/GetById/{result.Id}"), result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPut("{id:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult<WishViewModelResponse>> Update(Guid id, WishViewModelRequest model)
    {
        try
        {
            var wish = _mapper.Map<Wish>(model);
            wish.Id = id;

            var res = await _wishService.Update(wish);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<WishViewModelResponse>(res);

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
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _wishService.Delete(id);
            await _unitOfWork.SaveChangesAsync();

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
        finally
        {
            _unitOfWork.Dispose();
        }
    }
}
