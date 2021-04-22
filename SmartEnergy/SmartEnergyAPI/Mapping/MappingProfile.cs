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

        }
    }
}
