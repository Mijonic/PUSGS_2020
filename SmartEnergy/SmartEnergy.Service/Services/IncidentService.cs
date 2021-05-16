﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Device;
using SmartEnergy.Contract.CustomExceptions.DeviceUsage;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
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
        private readonly ITimeService _timeService;   
        private readonly IDeviceUsageService _deviceUsageService;
        private readonly ICallService _callService;
        private readonly IMapper _mapper;
        

        public IncidentService(SmartEnergyDbContext dbContext, ITimeService timeService, IDeviceUsageService deviceUsageService,  IMapper mapper, ICallService callService)
        {
            _dbContext = dbContext;
            _timeService = timeService;
            _deviceUsageService = deviceUsageService;
            _callService = callService;
            _mapper = mapper;


        }

           
        


        // determine what to delete with incident object
        public void Delete(int id)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.MultimediaAnchor)
                                                    .Include(x => x.NotificationAnchor)
                                                    .Include(x => x.IncidentDevices)
                                                    .Include(x => x.WorkRequest)
                                                    .FirstOrDefault(x => x.ID == id);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {id} dos not exist.");

            _dbContext.Incidents.Remove(incident);

           // Remove anchors
            _dbContext.MultimediaAnchors.Remove(incident.MultimediaAnchor);
            _dbContext.NotificationAnchors.Remove(incident.NotificationAnchor);
  

            _dbContext.SaveChanges();
        }

        public IncidentDto Get(int id)
        {
            Incident incident = _dbContext.Incidents.Find(id);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {id} does not exist.");

            return _mapper.Map<IncidentDto>(incident);
        }


        public List<IncidentDto> GetAll()
        {
            return _mapper.Map<List<IncidentDto>>(_dbContext.Incidents.ToList());
        }

        /// <summary>
        /// Get incident location
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>LocationDto</returns>
        public LocationDto GetIncidentLocation(int incidentId)
        {
            //TODO:
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                    .ThenInclude(p => p.Device)
                                                    .ThenInclude(o => o.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            DayPeriod currentDayPeriod = _timeService.GetCurrentDayPeriod();

            //Try getting location from devices
            List<DeviceUsage> devices = new List<DeviceUsage>();
            if (currentDayPeriod == DayPeriod.MORNING)
                devices = incident.IncidentDevices.OrderByDescending(x => x.Device.Location.MorningPriority).ToList();
            else if (currentDayPeriod == DayPeriod.NOON)
                devices = incident.IncidentDevices.OrderByDescending(x => x.Device.Location.NoonPriority).ToList();
            else
                devices = incident.IncidentDevices.OrderByDescending(x => x.Device.Location.NightPriority).ToList();

            foreach (DeviceUsage d in devices)
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
            ValidateIncident(entity);

            MultimediaAnchor mAnchor = new MultimediaAnchor();
   
            NotificationAnchor nAnchor = new NotificationAnchor();

            Incident incident = _mapper.Map<Incident>(entity);
            incident.ID = 0;
            incident.MultimediaAnchor = mAnchor;
            incident.NotificationAnchor = nAnchor;


            incident.Priority = 0;
            incident.IncidentStatus = IncidentStatus.INITIAL; // with basic info only init

         

        
            _dbContext.Incidents.Add(incident);

            _dbContext.SaveChanges();

            return _mapper.Map<IncidentDto>(incident);
        }

        public IncidentDto Update(IncidentDto entity)
        {
            ValidateIncident(entity);

            Incident oldIncident = _dbContext.Incidents.Find(entity.ID);

            if (oldIncident == null)
                throw new IncidentNotFoundException($"Incident with id {entity.ID} does not exist");

            oldIncident.Update(_mapper.Map<Incident>(entity));

            _dbContext.SaveChanges();
            
            return _mapper.Map<IncidentDto>(oldIncident);

        }



        private void ValidateIncident(IncidentDto entity)
        {

            if (!Enum.IsDefined(typeof(WorkType), entity.WorkType))
                throw new InvalidIncidentException("Undefined work type!");

            if (!Enum.IsDefined(typeof(IncidentStatus), entity.IncidentStatus))
                throw new InvalidIncidentException("Undefined incident status!");

            if (entity.Description == null || entity.Description.Length > 100)
                throw new InvalidIncidentException($"Description must be at most 100 characters long!");


            if (entity.VoltageLevel <= 0)
                throw new InvalidIncidentException("Voltage level have to be greater than 0!");

            //proveriti validacije za datume

            if (entity.IncidentDateTime > entity.ETA)
                throw new InvalidIncidentException($"ETA date cannot be before incident date!");

            if (entity.IncidentDateTime > entity.ATA)
                throw new InvalidIncidentException($"ATA date cannot be before incident date!");

         

            if (entity.WorkBeginDate < entity.IncidentDateTime)
                throw new InvalidIncidentException($"Sheduled date cannot be before incident date.");




        }
       
        
        
        /// <summary>
        /// Get priority for specific incident by finding device related to incident with highest priority.
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns>Integer priority</returns>
        private int GetIncidentPriority(int incidentId)
        {

            int priority = -1;

            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                     .ThenInclude(p => p.Device)
                                                     .ThenInclude(o => o.Location)
                                                     .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");


            List<int> allPriorities = new List<int>();

            DayPeriod currentDayPeriod = _timeService.GetCurrentDayPeriod();

            



            //Try getting location from devices
            foreach (DeviceUsage d in incident.IncidentDevices)
            {
                if (currentDayPeriod == DayPeriod.MORNING)
                    allPriorities.Add(d.Device.Location.MorningPriority);
                else if(currentDayPeriod == DayPeriod.NOON)
                    allPriorities.Add(d.Device.Location.NoonPriority);
                else
                    allPriorities.Add(d.Device.Location.NightPriority);

            }

            priority = allPriorities.Max();

            if (priority != -1)
                return priority;
            else
                return 0;

          

          
        }


        /// <summary>
        /// Connect specific crew with incident
        /// </summary>
        /// <param name="incidentId"></param>
        /// <param name="crewId"></param>
        /// <returns></returns>
        public IncidentDto AddCrewToIncident(int incidentId, int crewId)
        {
            Incident incident = _dbContext.Incidents.Find(incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id = {incidentId} does not exists!");


            Crew crew = _dbContext.Crews.Find(crewId);

            if (crew == null)
                throw new CrewNotFoundException($"Crew with id = {crewId} does not exists!");

            incident.CrewID = crew.ID;


            _dbContext.SaveChanges();

            return _mapper.Map<IncidentDto>(incident);







        }


        /// <summary>
        /// This function get all incidents which are ready for usage in work request
        /// </summary>
        /// <returns>List of ready incidents</returns>
        public List<IncidentDto> GetUnassignedIncidents()
        {

            bool isFree = true;

            List<Incident> allIncidents = _dbContext.Incidents.ToList();
            List<WorkRequest> allWorkRequests = _dbContext.WorkRequests.Include("Incident").ToList();

            List<Incident> unassignedIcidents = new List<Incident>();

            foreach(Incident incident in allIncidents)
            {
                isFree = true;

                foreach(WorkRequest workRequest in allWorkRequests)
                {
                    if (workRequest.IncidentID.Equals(incident.ID))
                    {
                        isFree = false;
                        break;
                    }
                   
                }

                if( !incident.IncidentStatus.Equals(IncidentStatus.INITIAL) && isFree)
                    unassignedIcidents.Add(incident);


            }


            
            return _mapper.Map<List<IncidentDto>>(unassignedIcidents);
        }

        public void AddDeviceToIncident(int incidentId, int deviceId)
        {

            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                    .ThenInclude(p => p.Device)
                                                    .ThenInclude(o => o.Location)
                                                    .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            Device device = _dbContext.Devices.Find(deviceId);

            if (device == null)
                throw new DeviceNotFoundException($"Device with id = { deviceId} does not exists!");


            if (incident.IncidentDevices.Find(x => x.DeviceID == deviceId) != null)
                throw new InvalidDeviceUsageException($"Device with id = {deviceId} is already added to incident!");



            _deviceUsageService.Insert(new DeviceUsageDto { IncidentID = incidentId, DeviceID = deviceId });

            incident.Priority = GetIncidentPriority(incident.ID);


            _dbContext.SaveChanges();






        }

        public IncidentDto RemoveCrewFromIncidet(int incidentId)
        {
            Incident incident = _dbContext.Incidents.Find(incidentId);

            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id = {incidentId} does not exists!");


            

            incident.CrewID = null;
            _dbContext.SaveChanges();


            return _mapper.Map<IncidentDto>(incident);
        }

        public void RemoveDeviceFromIncindet(int incidentId, int deviceId)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                   .ThenInclude(p => p.Device)
                                                   .ThenInclude(o => o.Location)
                                                   .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            Device device = _dbContext.Devices.Find(deviceId);

            if (device == null)
                throw new DeviceNotFoundException($"Device with id = { deviceId} does not exists!");


            DeviceUsage toRemove = incident.IncidentDevices.Find(x => x.DeviceID == deviceId);

            if ( toRemove == null)
                throw new InvalidDeviceUsageException($"Device with id = {deviceId} is not connected with incident with id = {incidentId}");


            
            _deviceUsageService.Delete(toRemove.ID);
            incident.Priority = GetIncidentPriority(incident.ID);


            _dbContext.SaveChanges();
        }

        public List<IncidentMapDisplayDto> GetUnresolvedIncidentsForMap()
        {
            List<Incident> incidents = _dbContext.Incidents.Include(x => x.Crew)
                                                           .Where(x => x.IncidentStatus == IncidentStatus.UNRESOLVED).ToList();
            List<IncidentMapDisplayDto> returnValue = new List<IncidentMapDisplayDto>();

            foreach(Incident incident in incidents)
            {
                try
                {
                    returnValue.Add(new IncidentMapDisplayDto()
                    {
                        ID = incident.ID,
                        IncidentDateTime = incident.IncidentDateTime,
                        Priority = incident.Priority,
                        Location = GetIncidentLocation(incident.ID),
                        Crew = _mapper.Map<CrewDto>(incident.Crew)
                    }) ;
                }catch
                {

                }
            }

            return returnValue;
        }

        public List<CallDto> GetIncidentCalls(int incidentId)
        {
            return _callService.GetAll().Where(x => x.IncidentID == incidentId).ToList();

        }

        public int GetNumberOfCalls(int incidentId)
        {
            return _callService.GetAll().Where(x => x.IncidentID == incidentId).Count();
        }

        public int GetNumberOfAffectedConsumers(int incidentId)
        {
            int affectedConsumers = 0;

            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                   .ThenInclude(p => p.Device)
                                                   .ThenInclude(o => o.Location)
                                                   .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            List<string> deviceStreets = new List<string>();

            foreach (DeviceUsage device in incident.IncidentDevices)
            {
                if (!deviceStreets.Contains(device.Device.Location.Street.ToLower().Trim()))
                    deviceStreets.Add(device.Device.Location.Street.ToLower().Trim());

            }

            List<Consumer> consumers = _dbContext.Consumers.Include("Location").ToList();

            foreach(string deviceStreet in deviceStreets)
            {
                foreach(Consumer consumer in consumers)
                {
                    if (consumer.Location.Street.ToLower().Trim().Equals(deviceStreet))
                        affectedConsumers++;
                }
            }

           



            return affectedConsumers;
            
        }

        public List<DeviceDto> GetIncidentDevices(int incidentId)
        {
            Incident incident = _dbContext.Incidents.Include(x => x.IncidentDevices)
                                                   .ThenInclude(p => p.Device)
                                                   .ThenInclude(o => o.Location)
                                                   .FirstOrDefault(x => x.ID == incidentId);
            if (incident == null)
                throw new IncidentNotFoundException($"Incident with id {incidentId} does not exist.");

            List<Device> incidentDevices = new List<Device>();

            foreach (DeviceUsage deviceUsage in incident.IncidentDevices)
                incidentDevices.Add(deviceUsage.Device);

            return _mapper.Map<List<DeviceDto>>(incidentDevices);

        }

        public void SetIncidentPriority(int incidentId)
        {
            int incidentPriority = GetIncidentPriority(incidentId);

            Incident incident = _dbContext.Incidents.Find(incidentId);

            incident.Priority = incidentPriority;
            _dbContext.SaveChanges();

        }
    }
}
