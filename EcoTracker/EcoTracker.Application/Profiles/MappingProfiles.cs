using AutoMapper;
using EcoTracker.Application.ViewModels;
using EcoTracker.Domain.Entities;

namespace EcoTracker.Application.Profiles
{
    public sealed class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AddUserViewModel, User>().ReverseMap();
            CreateMap<UpdateUserViewModel, User>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();


            CreateMap<AddWasteDisposalViewModel, WasteDisposal>().ReverseMap();
            CreateMap<UpdateWasteDisposalViewModel, WasteDisposal>().ReverseMap();
            CreateMap<WasteDisposalViewModel, WasteDisposal>().ReverseMap();

            CreateMap<AddPickUpScheduleViewModel, PickUpSchedule>().ReverseMap();
            CreateMap<UpdatePickUpScheduleViewModel, PickUpSchedule>().ReverseMap();
            CreateMap<PickUpScheduleViewModel, PickUpSchedule>().ReverseMap();

            CreateMap<User, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
