using AutoMapper;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
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
    public class WorkRequestService : IWorkRequestService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IIncidentService _incidentService;

        public WorkRequestService(SmartEnergyDbContext dbContext, IMapper mapper, IIncidentService incidentService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _incidentService = incidentService;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public WorkRequestDto Get(int id)
        {
            WorkRequest workRequest = _dbContext.WorkRequests.Find(id);

            if (workRequest == null)
                throw new WorkRequestNotFound($"Work request with id {id} does not exist.");

            return _mapper.Map<WorkRequestDto>(workRequest);
        }

        public List<WorkRequestDto> GetAll()
        {
            return _mapper.Map<List<WorkRequestDto>>(_dbContext.WorkRequests.ToList());
        }

        public WorkRequestDto Insert(WorkRequestDto entity)
        {
            ValidateWorkRequest(entity);

            MultimediaAnchor mAnchor = new MultimediaAnchor();

            StateChangeAnchor sAnchor = new StateChangeAnchor();

            NotificationAnchor nAnchor = new NotificationAnchor();

            WorkRequest workRequest = _mapper.Map<WorkRequest>(entity);
            workRequest.ID = 0;
            workRequest.MultimediaAnchor = mAnchor;
            workRequest.NotificationsAnchor = nAnchor;
            workRequest.StateChangeAnchor = sAnchor;
            workRequest.DocumentStatus = DocumentStatus.DRAFT;
            _dbContext.WorkRequests.Add(workRequest);

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(workRequest);
        }

        public WorkRequestDto Update(WorkRequestDto entity)
        {
            ValidateWorkRequest(entity);
            WorkRequest existing = _dbContext.WorkRequests.Find(entity.ID);
            if (existing == null)
                throw new WorkRequestNotFound($"Work request with id {entity.ID} does not exist");

            existing.Update(_mapper.Map<WorkRequest>(entity));

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(existing);

            
        }

        private void ValidateWorkRequest(WorkRequestDto entity)
        {
            if (_dbContext.Incidents.Find(entity.IncidentID) == null)
            {
                throw new IncidentNotFoundException($"Attached incident with id {entity.IncidentID} does not exist.");
            }

            if(_dbContext.Users.Find(entity.UserID) == null)
                throw new UserNotFoundException($"Attached user with id {entity.UserID} does not exist.");

            WorkRequest wr = _dbContext.WorkRequests.FirstOrDefault(x => x.IncidentID == entity.IncidentID);
            if (wr != null && wr.ID != entity.ID )
                throw new WorkRequestInvalidStateException($"Work request already created for incident with id {entity.IncidentID}");

            if (entity.StartDate.CompareTo(entity.EndDate) > 0)
                throw new WorkRequestInvalidStateException($"Start date cannot be after end date.");

            if (entity.StartDate.CompareTo(DateTime.Now) < 0)
                throw new WorkRequestInvalidStateException($"Start date cannot be in the past.");

            if (entity.Purpose == null || entity.Purpose.Length > 100)
                throw new WorkRequestInvalidStateException($"Purpose must be at most 100 characters long and is required.");

            if (entity.Note != null && entity.Note.Length > 100)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.Details != null && entity.Details.Length > 100)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.CompanyName != null && entity.CompanyName.Length > 50)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.Phone != null && entity.Phone.Length > 30)
                throw new WorkRequestInvalidStateException($"Phone must be at most 30 characters long.");

            if (entity.Street != null && entity.Street.Length > 50)
                throw new WorkRequestInvalidStateException($"Street must be at most 50 characters long.");

            if (entity.Street == null || entity.Street != "")
            {
                try
                {
                    LocationDto location = _incidentService.GetIncidentLocation(entity.IncidentID);
                    entity.Street = location.Street + ", " + location.City;
                }
                catch { }
            }
        }
    }
}
