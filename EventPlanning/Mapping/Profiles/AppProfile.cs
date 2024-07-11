using AutoMapper;
using Domain.Models;
using EventPlanning.Models.Event;
using EventPlanning.Models.EventField;
using EventPlanning.Models.FieldDescription;
using EventPlanning.Models.User;

namespace EventPlanning.Mapping.Profiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<EventViewModel, EventServiceModel>().ReverseMap();
            CreateMap<EventServiceModel, Event>().ReverseMap();

            CreateMap<UserControllerModel, User>().ReverseMap();


            CreateMap<FieldDescriptionServiceModel, FieldDescriptionViewModel>().ReverseMap();
            CreateMap<FieldDescription, FieldDescriptionServiceModel>().ReverseMap();

            CreateMap<EventFieldCreateModel, EventFieldWithValueServiceModel>();

            CreateMap<EventFieldWithValueServiceModel, EventField>().ReverseMap();

            CreateMap<Event, EventViewModel>()
                .ForMember(x=>x.CurrentMemberCount, opt=>opt.MapFrom(x=>x.Members.Count));
            CreateMap<User, UserViewModel>();
            CreateMap<EventField, EventFieldWithValueViewModel>();
            CreateMap<FieldDescription, FieldDescriptionViewModel>();

            CreateMap<EventCreateModel, EventServiceModel>();
            
            CreateMap<FieldDescription, FieldDescriptionViewModel>();
        }
    }
}
