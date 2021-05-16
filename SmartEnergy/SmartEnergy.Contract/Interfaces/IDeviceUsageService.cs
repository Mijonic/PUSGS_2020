using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IDeviceUsageService : IGenericService<DeviceUsageDto>
    {
        void CopyIncidentDevicesToWorkRequest(int incidentID, int workRequestID);
    }
}
