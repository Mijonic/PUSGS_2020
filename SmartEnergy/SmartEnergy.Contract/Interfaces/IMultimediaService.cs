using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IMultimediaService
    {
        public void AttachFileToWorkRequest(IFormFile formFile, int workRequestId);
    }
}
