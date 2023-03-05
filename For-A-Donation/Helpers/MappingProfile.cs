using AutoMapper;
using For_A_Donation.Models.DataBase;
using For_A_Donation.Models.ViewModels;

namespace For_A_Donation.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserViewModelRegistration, User>();

        CreateMap<UserViewModelRequest, User>();

        CreateMap<User, UserViewModelResponse>();

        CreateMap<UserProgress, UserProgressViewModelResponce>();
    }
}
