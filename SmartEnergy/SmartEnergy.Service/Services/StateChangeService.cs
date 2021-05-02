using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class StateChangeService : IStateChangeService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public StateChangeService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<StateChangeHistoryDto> GetWorkRequestStateHistory(int workRequestId)
        {
            WorkRequest workRequest = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                             .ThenInclude(x => x.StateChangeHistories)
                                                             .ThenInclude(x => x.User)
                                                             .FirstOrDefault(x => x.ID == workRequestId);

            if (workRequest == null)
                throw new WorkRequestNotFound($"Work request with ID {workRequestId} does not exist.");

            return _mapper.Map<List<StateChangeHistoryDto>>(workRequest.StateChangeAnchor.StateChangeHistories);
        }

        public void UpdateWorkRequestState(int workRequestId, DocumentStatus newState)
        {
            throw new NotImplementedException();
        }
    }
}
