using AutoMapper;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels;
using Task = For_A_Donation.Models.DataBase.Task;

namespace For_A_Donation.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserViewModelRegistration, User>();

        CreateMap<UserViewModelRequest, User>();

        CreateMap<User, UserViewModelResponse>();

        CreateMap<UserProgress, UserProgressViewModelResponce>();

        CreateMap<TaskViewModelRequest, Task>();

        CreateMap<Task, TaskViewModelResponse>();

        CreateMap<RewardViewModelRequest, Reward>();

        CreateMap<Reward, RewardViewModelResponse>();

        CreateMap<Reward, RewardListViewModelResponse>();

        CreateMap<ProgressViewModel, Progress>();

        CreateMap<Progress, ProgressViewModel>();
    }
}
