using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IStateChangeService
    {
        public WorkRequestDto ApproveWorkRequest(int workRequestId, ClaimsPrincipal user);
        public WorkRequestDto CancelWorkRequest(int workRequestId, ClaimsPrincipal user);
        public WorkRequestDto DenyWorkRequest(int workRequestId, ClaimsPrincipal user);
        public List<StateChangeHistoryDto> GetWorkRequestStateHistory(int workRequestId);
    }
}
