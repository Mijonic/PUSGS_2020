using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.CustomExceptions.Multimedia;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class MultimediaService : IMultimediaService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public MultimediaService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void AttachFileToWorkRequest(IFormFile formFile, int workRequestId)
        {
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestId);

            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");

            string filePath = Path.Combine(@$"Attachments/WR{workRequestId}/", formFile.FileName);
            if(_dbContext.MultimediaAttachments.FirstOrDefault(x => x.MultimediaAnchorID == wr.MultimediaAnchorID
                                                               && x.Url == formFile.FileName) != null)
            {
                throw new MultimediaAlreadyExists($"Attachment with name {formFile.FileName} is already attached to this work request.");
            }
            new FileInfo(filePath).Directory?.Create();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);

            }

            MultimediaAttachment attachment = new MultimediaAttachment();
            attachment.Url = formFile.FileName;
            attachment.MultimediaAnchorID = (int)wr.MultimediaAnchorID;
            _dbContext.MultimediaAttachments.Add(attachment);

            _dbContext.SaveChanges();


        }

        public void DeleteWorkRequestAttachment(int workRequestId, string filename)
        {
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestId);

            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");

            MultimediaAttachment attachment = _dbContext.MultimediaAttachments.FirstOrDefault(x => x.MultimediaAnchorID == wr.MultimediaAnchorID
                                                                                                && x.Url == filename);

            if (attachment == null)
                throw new MultimediaNotFoundException($"Work request with ID {workRequestId} does not contain file wiht name {filename}");

            _dbContext.MultimediaAttachments.Remove(attachment);

            if (File.Exists(@$"Attachments/WR{workRequestId}/{filename}"))
            {
                File.Delete(@$"Attachments/WR{workRequestId}/{filename}");
            }

            _dbContext.SaveChanges();
        }

        public List<MultimediaAttachmentDto> GetWorkRequestAttachments(int workRequestId)
        {
            WorkRequest wr = _dbContext.WorkRequests.Include(x => x.MultimediaAnchor)
                                                        .ThenInclude(x => x.MultimediaAttachments)
                                                        .FirstOrDefault(x => x.ID == workRequestId);
            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist");

            return _mapper.Map<List<MultimediaAttachmentDto>>(wr.MultimediaAnchor.MultimediaAttachments);
        }


        public FileStream GetWorkRequestAttachmentStream(int workRequestId, string fileName)
        {
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestId);
            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");

            MultimediaAttachment attachment = _dbContext.MultimediaAttachments.Where(x => x.MultimediaAnchorID == wr.MultimediaAnchorID &&
                                                                                     x.Url == fileName).FirstOrDefault();
            if (attachment == null)
                throw new MultimediaNotFoundException($"Multimedia attachment with name {fileName} does not exist.");
            FileStream stream = new FileStream(@$"Attachments/WR{workRequestId}/{fileName}", FileMode.Open);
            return stream;
        }
    }
}
