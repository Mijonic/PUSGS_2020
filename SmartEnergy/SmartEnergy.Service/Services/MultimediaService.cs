using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using nClam;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Multimedia;
using SmartEnergy.Contract.CustomExceptions.User;
using SmartEnergy.Contract.CustomExceptions.WorkRequest;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartEnergy.Service.Services
{
    public class MultimediaService : IMultimediaService
    {
        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public MultimediaService(SmartEnergyDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task AttachFileToWorkRequestAsync(IFormFile formFile, int workRequestId)
        {
            await ScanAttachmentAsync(formFile);
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestId);

            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");

            if (wr.DocumentStatus == DocumentStatus.APPROVED || wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Cannot attach to this work request as it is already {wr.DocumentStatus}");

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

        public async Task AttachUserAvatar(IFormFile formFile, int userId)
        {
            await ScanAttachmentAsync(formFile);

            User user = _dbContext.Users.Find(userId);

            if (!IsImage(formFile))
                throw new MultimediaNotImageException($"Uploaded file is not an image!");

            if (user == null)
                throw new UserNotFoundException($"User with ID {userId} does not exist.");

            string filePath = Path.Combine(@$"Attachments/Users/User{userId}/", formFile.FileName);
            if(Directory.Exists(@$"Attachments/Users/User{userId}"))
            {
                Directory.Delete(@$"Attachments/Users/User{userId}");
            }

            new FileInfo(filePath).Directory?.Create();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(stream);

            }

            user.ImageURL = formFile.FileName;

            _dbContext.SaveChanges();
        }

        public void DeleteWorkRequestAttachment(int workRequestId, string filename)
        {
            WorkRequest wr = _dbContext.WorkRequests.Find(workRequestId);

            if (wr == null)
                throw new WorkRequestNotFound($"Work request with id {workRequestId} does not exist.");

            if (wr.DocumentStatus == DocumentStatus.APPROVED || wr.DocumentStatus == DocumentStatus.CANCELLED)
                throw new WorkRequestInvalidStateException($"Cannot delete attachment from this work request as it is already {wr.DocumentStatus}");

            MultimediaAttachment attachment = _dbContext.MultimediaAttachments.FirstOrDefault(x => x.MultimediaAnchorID == wr.MultimediaAnchorID
                                                                                                && x.Url == filename);

            if (attachment == null)
                throw new MultimediaNotFoundException($"Work request with ID {workRequestId} does not contain file with name {filename}");

            _dbContext.MultimediaAttachments.Remove(attachment);

            if (File.Exists(@$"Attachments/WR{workRequestId}/{filename}"))
            {
                File.Delete(@$"Attachments/WR{workRequestId}/{filename}");
            }

            _dbContext.SaveChanges();
        }

        public FileStream GetUserAvatarStream(int userId, string imageURL)
        {
            User user = _dbContext.Users.Find(userId);
            if (user == null)
                throw new UserNotFoundException($"User with id {userId} does not exist");

            if (user.ImageURL != imageURL)
                throw new MultimediaNotFoundException($"User does not have profile picture!");

            FileStream stream = new FileStream(@$"Attachments/Users/User{userId}/{imageURL}", FileMode.Open);
            return stream;
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

        public async Task ScanAttachmentAsync(IFormFile formFile)
        {
            MemoryStream ms = new MemoryStream();
            formFile.OpenReadStream().CopyTo(ms);
            byte[] fileBytes = ms.ToArray();


            ClamClient clam = new ClamClient(this._configuration["ClamAVServer:URL"],
                                        Convert.ToInt32(this._configuration["ClamAVServer:Port"]));
            var scanResult = await clam.SendAndScanFileAsync(fileBytes);
           if (scanResult.Result != ClamScanResults.Clean)
                throw new MultimediaInfectedException($"This attachment is infected with virus!");
        }

        private bool IsImage(IFormFile postedFile)
        {
            const int ImageMinimumBytes = 512;
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.OpenReadStream().CanRead)
                {
                    return false;
                }
                //------------------------------------------
                //check whether the image size exceeding the limit or not
                //------------------------------------------ 
                if (postedFile.Length < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[ImageMinimumBytes];
                postedFile.OpenReadStream().Read(buffer, 0, ImageMinimumBytes);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new Bitmap(postedFile.OpenReadStream()))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                postedFile.OpenReadStream().Position = 0;
            }

            return true;
        }

    }
}
