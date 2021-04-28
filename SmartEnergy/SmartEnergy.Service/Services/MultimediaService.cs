using Microsoft.AspNetCore.Http;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class MultimediaService : IMultimediaService
    {
        private readonly SmartEnergyDbContext _dbContext;

        public MultimediaService(SmartEnergyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AttachFileToWorkRequest(IFormFile formFile, int workRequestId)
        {
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestId);

            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");

            var filePath = Path.Combine(@"Attachments", formFile.FileName);
            new FileInfo(filePath).Directory?.Create();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);

            }

            MultimediaAttachment attachment = new MultimediaAttachment();
            attachment.Url = formFile.FileName;
            attachment.MultimediaAnchorID = (int)wr.MultimediaAnchorID;
            _dbContext.MultimediaAttachments.Add(attachment);

            _dbContext.SaveChanges();


        }
    }
}
