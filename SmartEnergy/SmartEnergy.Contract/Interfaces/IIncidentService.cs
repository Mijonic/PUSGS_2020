using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IIncidentService : IGenericService<IncidentDto>
    {
        Task<LocationDto> GetIncidentLocation(int incidentId);

     
        IncidentDto AddCrewToIncident(int incidentId, int crewId);

        IncidentDto RemoveCrewFromIncidet(int incidentId);

        List<IncidentDto> GetUnassignedIncidents();

        void AddDeviceToIncident(int incidentId, int deviceId);

        void RemoveDeviceFromIncindet(int incidentId, int deviceId);
        Task<List<IncidentMapDisplayDto>> GetUnresolvedIncidentsForMap();

        Task<List<CallDto>> GetIncidentCalls(int incidentId);

        Task<int> GetNumberOfCalls(int incidentId);

        Task<int> GetNumberOfAffectedConsumers(int incidentId);

        Task<List<DeviceDto>> GetIncidentDevices(int incidentId);

        void SetIncidentPriority(int incidentId);

        Task<List<DeviceDto>> GetUnrelatedDevices(int incidentId);

        CrewDto GetIncidentCrew(int incidentId);

        CallDto AddIncidentCall(int incidentId, CallDto newCall);

        void AssignIncidetToUser(int incidentId, int userId);


        IncidentListDto GetIncidentsPaged(IncidentFields sortBy, SortingDirection direction, int page,
                               int perPage, IncidentFilter filter, OwnerFilter owner,
                               string searchParam, ClaimsPrincipal user);

         IncidentStatisticsDto GetStatisticsForUser(int userId);




    }
}
