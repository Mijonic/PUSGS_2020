using Microsoft.AspNetCore.Http;
using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IMultimediaService
    {
        public void AttachFileToWorkRequest(IFormFile formFile, int workRequestId);
        public FileStream GetWorkRequestAttachmentStream(int workRequestId, string fileName);
        public List<MultimediaAttachmentDto> GetWorkRequestAttachments(int workRequestId);
        public void DeleteWorkRequestAttachment(int workRequestId, string filename);
    }
}
