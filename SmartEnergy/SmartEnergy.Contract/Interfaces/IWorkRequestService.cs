using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IWorkRequestService :IGenericService<WorkRequestDto>
    {
        public List<DeviceDto> GetWorkRequestDevices(int workRequestId);
        public WorkRequestsListDto GetWorkRequestsPaged(WorkRequestField sortBy, SortingDirection direction, int page,
                                  int perPage, DocumentStatusFilter status, OwnerFilter owner,
                                  string searchParam, ClaimsPrincipal user);

        public WorkRequestStatisticsDto GetStatisticsForUser(int userId);
        public bool IsCrewMemberHandlingWorkRequest(int crewMemberId, int workRequestId);
    }
}
