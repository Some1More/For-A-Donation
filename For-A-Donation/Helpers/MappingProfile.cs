using AutoMapper;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels.Family;
using For_A_Donation.Models.ViewModels.Purpose;
using For_A_Donation.Models.ViewModels.Reward;
using For_A_Donation.Models.ViewModels.Task;
using For_A_Donation.Models.ViewModels.User;
using For_A_Donation.Models.ViewModels.Wish;
using Task = For_A_Donation.Models.DataBase.Task;

namespace For_A_Donation.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserViewModelRegistration, User>();

        CreateMap<UserViewModelRequest, User>();

        CreateMap<User, UserViewModelResponse>();

        CreateMap<User, UserListViewModelResponse>();

        CreateMap<UserProgress, UserProgressViewModelResponce>();

        CreateMap<TaskViewModelRequest, Task>();

        CreateMap<Task, TaskViewModelResponse>();

        CreateMap<RewardViewModelRequest, Reward>();

        CreateMap<Reward, RewardViewModelResponse>();

        CreateMap<Reward, RewardListViewModelResponse>();

        CreateMap<ProgressViewModel, Progress>();

        CreateMap<Progress, ProgressViewModel>();

        CreateMap<FamilyViewModelRequest, Family>();

        CreateMap<Family, FamilyViewModelResponse>();

        CreateMap<Purpose, PurposeViewModelResponse>();

        CreateMap<PurposeViewModelRequest, Purpose>();

        CreateMap<WishViewModelRequest, Wish>();

        CreateMap<Wish, WishViewModelResponse>();
    }
}
