using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.DomainModels
{
    public class MultimediaAnchor
    {
        public int ID { get; set; }
        public List<MultimediaAttachment> MultimediaAttachments { get; set; }

    }
}
