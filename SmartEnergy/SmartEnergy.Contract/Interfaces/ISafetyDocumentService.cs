using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ISafetyDocumentService : IGenericService<SafetyDocumentDto>
    {

        CrewDto GetCrewForSafetyDocument(int safetyDocumentId);

        ChecklistDto UpdateSafetyDocumentChecklist(ChecklistDto checklistDto);

        List<DeviceDto> GetSafetyDocumentDevices(int safetyDocumentId);

        List<SafetyDocumentDto> GetAllMineSafetyDocuments(OwnerFilter owner, ClaimsPrincipal user);
     



    }
}
