using AutoMapper;
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

        public WorkRequestService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
            if(_dbContext.Incidents.Find(entity.IncidentID) == null)
            {
                throw new IncidentNotFoundException($"Attached incident with id {entity.IncidentID} does not exist.");
            }

            if (entity.StartDate.CompareTo(entity.EndDate) > 0)
                throw new WorkRequestInvalidStateException($"Start date cannot be after end date.");

            if(entity.Purpose == null || entity.Purpose.Length >100)
                throw new WorkRequestInvalidStateException($"Purpose must be at most 100 characters long and is required.");

            if (entity.Note != null && entity.Note.Length > 100)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.CompanyName != null && entity.CompanyName.Length > 50)
                throw new WorkRequestInvalidStateException($"Note must be at most 100 characters long.");

            if (entity.Phone != null && entity.Phone.Length > 30)
                throw new WorkRequestInvalidStateException($"Phone must be at most 30 characters long.");

            if (entity.Street == null || entity.Street.Length > 50)
                throw new WorkRequestInvalidStateException($"Street must be at most 50 characters long and is required.");

            MultimediaAnchor mAnchor = new MultimediaAnchor();
            _dbContext.MultimediaAnchors.Add(mAnchor);

            StateChangeAnchor sAnchor = new StateChangeAnchor();
            _dbContext.StateChangeAnchors.Add(sAnchor);

            NotificationAnchor nAnchor = new NotificationAnchor();
            _dbContext.NotificationAnchors.Add(nAnchor);

            WorkRequest workRequest = _mapper.Map<WorkRequest>(entity);
            workRequest.ID = 0;
            workRequest.MultimediaAnchorID = mAnchor.ID;
            workRequest.NotificationAnchorID = nAnchor.ID;
            workRequest.StateChangeAnchorID = sAnchor.ID;
            workRequest.DocumentStatus = DocumentStatus.DRAFT;
            _dbContext.WorkRequests.Add(workRequest);

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(workRequest);
        }

        public WorkRequestDto Update(WorkRequestDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
