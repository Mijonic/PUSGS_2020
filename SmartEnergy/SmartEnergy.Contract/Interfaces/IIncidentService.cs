using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IIncidentService : IGenericService<IncidentDto>
    {
        LocationDto GetIncidentLocation(int incidentId);

        IncidentDto AddCrewToIncident(int incidentId, int crewId);

        List<IncidentDto> GetUnassignedIncidents();

        void AddDeviceToIncident(int incidentId, int deviceId);

        List<IncidentMapDisplayDto> GetUnresolvedIncidentsForMap();

        
    }
}
