using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class IncidentService : IIncidentService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public IncidentService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IncidentDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<IncidentDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public LocationDto GetIncidentLocation(int incidentId)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                    .ThenInclude(p => p.Device)
                                                    .ThenInclude(o => o.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            //Try getting location from devices
            foreach (DeviceUsage d in incident.IncidentDevices)
            {
                return _mapper.Map<LocationDto>(d.Device.Location);
            }

            incident = _dbContext.Incidents.Include(x => x.Calls)
                                                    .ThenInclude(p => p.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            //Try getting location from calls
            foreach (Call c in incident.Calls)
            {
                return _mapper.Map<LocationDto>(c.Location);
            }

            throw new LocationNotFoundException($"Location does not exist for incident with id {incidentId}");
        }

        public IncidentDto Insert(IncidentDto entity)
        {
            throw new NotImplementedException();
        }

        public IncidentDto Update(IncidentDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
