using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IStateChangeService
    {
        public WorkRequestDto ApproveWorkRequest(int workRequestId);
        public WorkRequestDto CancelWorkRequest(int workRequestId);
        public WorkRequestDto DenyWorkRequest(int workRequestId);
        public List<StateChangeHistoryDto> GetWorkRequestStateHistory(int workRequestId);
    }
}
