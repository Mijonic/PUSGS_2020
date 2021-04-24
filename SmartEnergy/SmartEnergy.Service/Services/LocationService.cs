using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;

namespace SmartEnergy.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationDto GetAllLocations()
        {
            return _mapper.Map<LocationDto>(_dbContext.Location.ToList());
        }
    }
}
