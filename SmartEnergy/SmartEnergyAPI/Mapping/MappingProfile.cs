using AutoMapper;
using SmartEnergy.Contract.DTO;
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

        }
    }
}
