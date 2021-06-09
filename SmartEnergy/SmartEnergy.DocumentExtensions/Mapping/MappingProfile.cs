using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.DocumentExtensions.DomainModels;
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

            CreateMap<MultimediaAttachmentDto, MultimediaAttachment>();
            CreateMap<MultimediaAttachment, MultimediaAttachmentDto>();
            CreateMap<StateChangeHistory, StateChangeHistoryDto>()
            .ForMember(mem => mem.DocumentStatus, op => op.MapFrom(o => o.DocumentStatus));

        }
    }
}
