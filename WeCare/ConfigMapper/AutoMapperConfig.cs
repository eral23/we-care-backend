using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeCare.Dto;
using WeCare.Entities;
using WeCare.Entities.Identity;
using WeCare.Util;

namespace WeCare.ConfigMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Specialist,SpecialistDto>();
            CreateMap<DataCollection<Specialist>, DataCollection<SpecialistDto>>();
            CreateMap<Specialist, SpecialistSimpleDto>();
            CreateMap<DataCollection<Specialist>, DataCollection<SpecialistSimpleDto>>();

            CreateMap<Patient, PatientDto>();
            CreateMap<DataCollection<Patient>, DataCollection<PatientDto>>();
            CreateMap<Patient, PatientSimpleDto>();
            CreateMap<DataCollection<Patient>, DataCollection<PatientSimpleDto>>();

            CreateMap<Event, EventDto>();
            CreateMap<DataCollection<Event>, DataCollection<EventDto>>();
            CreateMap<List<Event>, DataCollection<Event>>();
            CreateMap<Event, EventSimpleDto>();
            CreateMap<DataCollection<Event>, DataCollection<EventSimpleDto>>();
            CreateMap<List<EventSimpleDto>, DataCollection<EventSimpleDto>>();

            CreateMap<State, StateDto>();
            CreateMap<DataCollection<State>, DataCollection<StateDto>>();
            CreateMap<List<State>, DataCollection<State>>();
            CreateMap<State, StateSimpleDto>();
            CreateMap<DataCollection<State>, DataCollection<StateSimpleDto>>();
            CreateMap<List<StateSimpleDto>, DataCollection<StateSimpleDto>>();

            CreateMap<ApplicationUser, ApplicationUserDto>().ForMember(dest => dest.Roles,
                opts => opts.MapFrom(src => src.UserRoles.Select(y => y.Role.Name).ToList()));
            CreateMap<DataCollection<Specialist>, DataCollection<SpecialistDto>>();
        }
    }
}
