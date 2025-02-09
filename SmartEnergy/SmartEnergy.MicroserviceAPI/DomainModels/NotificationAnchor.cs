﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class NotificationAnchor
    {
        public int ID  {get; set;}
        public Incident Incident { get; set; }
        public WorkRequest WorkRequest { get; set; }
        public WorkPlan WorkPlan { get; set; }
        public SafetyDocument SafetyDocument { get; set; }
        public List<Notification> Notifications { get; set; }

    }
}
