using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergyAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Icon, IconDto>();
            CreateMap<Settings, SettingsDto>();

            CreateMap<IconDto, Icon>();
            CreateMap<SettingsDto, Settings>();

            CreateMap<CrewDto, Crew>();
            CreateMap<Crew, CrewDto>();

            CreateMap<UserDto, User>()
                .ForMember(mem => mem.UserStatus, op => op.MapFrom(o => o.UserStatus))
                .ForMember(mem => mem.UserType, op => op.MapFrom(o => o.UserType)); 
            CreateMap<User, UserDto>()
                .ForMember(mem => mem.UserStatus, op => op.MapFrom(o => o.UserStatus))
                .ForMember(mem => mem.UserType, op => op.MapFrom(o => o.UserType));

            CreateMap<LocationDto, Location>();
            CreateMap<Location, LocationDto>();

            CreateMap<DeviceDto, Device>()
                .ForMember(mem => mem.DeviceType, op => op.MapFrom(o => o.DeviceType));
            CreateMap<Device, DeviceDto>()
                .ForMember(mem => mem.DeviceType, op => op.MapFrom(o => o.DeviceType));

            CreateMap<WorkRequestDto, WorkRequest>()
                .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
                .ForMember(mem => mem.DocumentType, op => op.MapFrom(o => o.DocumentType));

            CreateMap<WorkRequest, WorkRequestDto>()
                .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus))
                .ForMember(mem => mem.DocumentType, op => op.MapFrom(o => o.DocumentType));

            CreateMap<Incident, IncidentDto>()
               .ForMember(mem => mem.WorkType, op => op.MapFrom(o => o.WorkType))
               .ForMember(mem => mem.IncidentStatus, op => op.MapFrom(o => o.IncidentStatus));

            CreateMap<IncidentDto, Incident>()
             .ForMember(mem => mem.WorkType, op => op.MapFrom(o => o.WorkType))
             .ForMember(mem => mem.IncidentStatus, op => op.MapFrom(o => o.IncidentStatus));

        }
    }
}
