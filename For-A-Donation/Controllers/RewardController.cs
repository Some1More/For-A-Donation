using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Helpers.Attributes;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using For_A_Donation.Models.ViewModels.Reward;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace For_A_Donation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class RewardController : ControllerBase
{
    private readonly IRewardService _rewardService;
    private readonly IUserProgressService _userProgressService;
    private readonly IMapper _mapper;

    public RewardController(IRewardService rewardService,
                            IUserProgressService userProgressService,
                            IMapper mapper)
    {
        _rewardService = rewardService;
        _userProgressService = userProgressService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(new string[] { "Father", "Mother", "Son", "Daughter", "Grandfather", "Grandmother" })]
    public ActionResult< List<RewardListViewModelResponse> > GetAll()
    {
        var res = _rewardService.GetAll();
        var result = _mapper.Map<List<RewardListViewModelResponse>>(res);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:Guid}")]
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
    }

    [HttpGet]
    [Route("{categoryNumber:int}")]
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
    }

    [HttpPost]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< RewardViewModelResponse >> Create(RewardViewModelRequest model)
    {
        var reward = _mapper.Map<Reward>(model);
        var res = await _rewardService.Create(reward);
        var result = _mapper.Map<RewardViewModelResponse>(res);

        return Created(new Uri($"https://localhost:7006/api/Reward/GetById/{result.Id}"), result);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(new string[] { "Father", "Mother", "Grandfather", "Grandmother" })]
    public async Task<ActionResult< RewardViewModelResponse >> Update(Guid id, RewardViewModelRequest model)
    {
        try
        {
            var reward = _mapper.Map<Reward>(model);
            reward.Id = id;

            var res = await _rewardService.Update(reward);
            var result = _mapper.Map<RewardViewModelResponse>(res);

            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:Guid},{userId:Guid}")]
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
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _rewardService.Delete(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
