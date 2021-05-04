using Microsoft.AspNetCore.Http;
using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IMultimediaService
    {
        public Task AttachFileToWorkRequestAsync(IFormFile formFile, int workRequestId);
        public Task ScanAttachmentAsync(IFormFile formFile);
        public FileStream GetWorkRequestAttachmentStream(int workRequestId, string fileName);
        public List<MultimediaAttachmentDto> GetWorkRequestAttachments(int workRequestId);
        public void DeleteWorkRequestAttachment(int workRequestId, string filename);
    }
}
