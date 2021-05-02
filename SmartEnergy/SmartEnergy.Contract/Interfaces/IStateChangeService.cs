using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IStateChangeService
    {
        public void UpdateWorkRequestState(int workRequestId, DocumentStatus newState);
        public List<StateChangeHistoryDto> GetWorkRequestStateHistory(int workRequestId);
    }
}
