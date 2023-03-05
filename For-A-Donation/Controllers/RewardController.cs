using AutoMapper;
using For_A_Donation.Exceptions;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.Enums;
using For_A_Donation.Models.ViewModels;
using For_A_Donation.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task = System.Threading.Tasks.Task;

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
    public ActionResult< List<RewardListViewModelResponse> > GetAll()
    {
        var res = _rewardService.GetAll();
        var result = _mapper.Map<List<RewardListViewModelResponse>>(res);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public ActionResult< RewardViewModelResponse > GetById(int id)
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
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{name}")]
    public ActionResult< RewardViewModelResponse > GetByName(string name)
    {
        try
        {
            var res = _rewardService.GetByName(name);
            var result = _mapper.Map<RewardViewModelResponse>(res);

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
    [Route("{categoryNumber:int}")]
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
    public async Task<ActionResult< RewardViewModelResponse >> Create(RewardViewModelRequest model)
    {
        try
        {
            var reward = _mapper.Map<Reward>(model);
            var res = await _rewardService.Create(reward);
            var result = _mapper.Map<RewardViewModelResponse>(res);

            return Created(new Uri(""), result);
        }
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult< RewardViewModelResponse >> Update(int id, RewardViewModelRequest model)
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
        catch (ObjectNotUniqueException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{id:int},{userId:int}")]
    public async Task<ActionResult> GottenReward(int id, int userId)
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
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id)
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
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
