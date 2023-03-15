using AutoMapper;
using For_A_Donation.Domain.Core.Enums;
using For_A_Donation.Domain.Core.Models;
using For_A_Donation.Domain.Interfaces;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.ViewModels.Reward;
using For_A_Donation.Services.Interfaces;
using For_A_Donation.Services.Interfaces.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RewardController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRewardService _rewardService;
    private readonly IProgressService _progressService;
    private readonly IUserProgressService _userProgressService;
    private readonly IMapper _mapper;

    public RewardController(IRewardService rewardService,
                            IUserProgressService userProgressService,
                            IProgressService progressService,
                            IUnitOfWork unitOfWork,
                            IMapper mapper)
    {
        _rewardService = rewardService;
        _userProgressService = userProgressService;
        _progressService = progressService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<RewardListViewModelResponse> > GetAll()
    {
        try
        {
            var res = _rewardService.GetAll();
            var result = _mapper.Map<List<RewardListViewModelResponse>>(res);

            return Ok(result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpGet("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< RewardViewModelResponse > GetById(Guid id)
    {
        try
        {
            var res = _rewardService.GetById(id);
            var result = _mapper.Map<RewardViewModelResponse>(res);

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

    [HttpGet("{name}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<RewardListViewModelResponse >> GetByName(string name)
    {
        try
        {
            var res = _rewardService.GetByName(name);
            var result = _mapper.Map<RewardListViewModelResponse>(res);

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

    [HttpGet("{categoryNumber:int}")]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<RewardListViewModelResponse> > GetByCategory(int categoryNumber)
    {
        try
        {
            var category = (CategoryOfReward)Enum.ToObject(typeof(CategoryOfReward), categoryNumber);
            var res = _rewardService.GetByCategory(category);
            var result = _mapper.Map<List<RewardListViewModelResponse>>(res);

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
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPost]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< RewardViewModelResponse >> Create(RewardViewModelRequest model)
    {
        try
        {
            var reward = _mapper.Map<Reward>(model);

            var res = await _rewardService.Create(reward);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<RewardViewModelResponse>(res);

            return Created(new Uri($"https://localhost:7006/api/Reward/GetById/{result.Id}"), result);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    [HttpPut("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< RewardViewModelResponse >> Update(Guid id, RewardViewModelRequest model)
    {
        try
        {
            var reward = _mapper.Map<Reward>(model);
            reward.Id = id;

            await _progressService.DeleteListByUserId(id);
            var res = await _rewardService.Update(reward);
            await _unitOfWork.SaveChangesAsync();

            var result = _mapper.Map<RewardViewModelResponse>(res);

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

    [HttpPut("{id:Guid},{userId:Guid}")]
    [Authorize(new string[] { "Son", "Daughter" })]
    public async Task<ActionResult> GottenReward(Guid id, Guid userId)
    {
        try
        {
            var reward = await _rewardService.GottenReward(id);

            var progress = _userProgressService.GetByUserId(userId);

            foreach(Progress pr in reward.Progress)
            {
                var oneProgress = progress.Single(x => x.CategoryOfTask == pr.CategoryOfTask);
                oneProgress.Points -= pr.PointsEnd; 

                await _userProgressService.Update(oneProgress);
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok();
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

    [HttpDelete("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _rewardService.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
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
