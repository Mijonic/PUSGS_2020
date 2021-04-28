using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IWorkRequestService :IGenericService<WorkRequestDto>
    {
        public List<DeviceDto> GetWorkRequestDevices(int workRequestId);
    }
}
