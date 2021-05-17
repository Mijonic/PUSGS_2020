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
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class StateChangeService : IStateChangeService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthHelperService _authHelperService;

        public StateChangeService(SmartEnergyDbContext dbContext, IMapper mapper, IAuthHelperService authHelperService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authHelperService = authHelperService;
        }

        public WorkRequestDto ApproveWorkRequest(int workRequestId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == workRequestId);

            if (wr == null)
                throw new WorkRequestInvalidStateException($"Work request with ID {workRequestId} deos not exist");

            if(wr.DocumentStatus == DocumentStatus.APPROVED)
                throw new WorkRequestInvalidStateException($"Work request is already approved.");

            if (wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Work request is canceled and cannot be approved.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID, 
                DocumentStatus = DocumentStatus.APPROVED
            };


            wr.StateChangeAnchor.StateChangeHistories.Add(state);
            wr.DocumentStatus = DocumentStatus.APPROVED;

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(wr);
        }

        public WorkRequestDto CancelWorkRequest(int workRequestId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == workRequestId);

            if (wr == null)
                throw new WorkRequestInvalidStateException($"Work request with ID {workRequestId} deos not exist");

            if (wr.DocumentStatus == DocumentStatus.APPROVED)
                throw new WorkRequestInvalidStateException($"Work request is approved and cannot be canceled.");

            if (wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Work request is already canceled.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID,
                DocumentStatus = DocumentStatus.CANCELLED
            };


            wr.StateChangeAnchor.StateChangeHistories.Add(state);
            wr.DocumentStatus = DocumentStatus.CANCELLED;

            _dbContext.SaveChanges();
            return _mapper.Map<WorkRequestDto>(wr);
        }

        public WorkRequestDto DenyWorkRequest(int workRequestId, ClaimsPrincipal user)
        {
            int userID = _authHelperService.GetUserIDFromPrincipal(user);
            // TODO: Add user after authentication! 
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.StateChangeAnchor)
                                                    .ThenInclude(x => x.StateChangeHistories)
                                                    .FirstOrDefault(x => x.ID == workRequestId);

            if (wr == null)
                throw new WorkRequestInvalidStateException($"Work request with ID {workRequestId} deos not exist");

            if (wr.DocumentStatus == DocumentStatus.APPROVED)
                throw new WorkRequestInvalidStateException($"Work request is approved and cannot be denied.");
            
            if (wr.DocumentStatus == DocumentStatus.DENIED)
                throw new WorkRequestInvalidStateException($"Work request is already denied.");

            if (wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Work request is canceled and cannot be denied.");

            StateChangeHistory state = new StateChangeHistory()
            {
                UserID = userID, 
                DocumentStatus = DocumentStatus.DENIED
            };


            wr.StateChangeAnchor.StateChangeHistories.Add(state);
            wr.DocumentStatus = DocumentStatus.DENIED;

            _dbContext.SaveChanges();

            return _mapper.Map<WorkRequestDto>(wr);
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
    }
}
