using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels.Wish;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class WishController : ControllerBase
{
    private readonly IWishService _wishService;
    private readonly IMapper _mapper;

    public WishController(IWishService wishService, IMapper mapper)
    {
        _wishService = wishService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult<List<WishViewModelResponse>> GetAll()
    {
        var res = _wishService.GetAll();
        var result = _mapper.Map<List<WishViewModelResponse>>(res);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:Guid}")]
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
    }

    [HttpGet]
    public ActionResult<List<WishViewModelResponse>> GetByFilter(WithFilterViewModel model)
    {
        try
        {   
            var res = _wishService.GetByFilter(model.Category, model.UserId);
            var result = _mapper.Map<List<WishViewModelResponse>>(res);

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
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult<WishViewModelResponse>> Create(WishViewModelRequest model)
    {
        var wish = _mapper.Map<Wish>(model);
        var res = await _wishService.Create(wish);
        var result = _mapper.Map<WishViewModelResponse>(res);

        return Created(new Uri($"https://localhost:7006/api/Wish/GetById/{result.Id}"), result);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult<WishViewModelResponse>> Update(Guid id, WishViewModelRequest model)
    {
        try
        {
            var wish = _mapper.Map<Wish>(model);
            wish.Id = id;

            var res = await _wishService.Update(wish);
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
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _wishService.Delete(id);
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
