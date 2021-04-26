using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IIncidentService : IGenericService<IncidentDto>
    {
        LocationDto GetIncidentLocation(int incidentId);
    }
}
