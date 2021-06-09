using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.DomainModels
{
    public class NotificationAnchor
    {
        public int ID  {get; set;}
        public List<Notification> Notifications { get; set; }

    }
}
