﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.DomainModels
{
    public class DeviceUsage
    {
        public int ID { get; set; }
        public int? IncidentID { get; set; }
        public Incident Incident { get; set; }
        public int? WorkRequestID { get; set; }
        public WorkRequest WorkRequest { get; set; }
        public int? WorkPlanID { get; set; }
        public WorkPlan WorkPlan { get; set; }
        public int? SafetyDocumentID { get; set; }
        public SafetyDocument SafetyDocument { get; set; }
        public int DeviceID { get; set; }
        //public Device Device { get; set; }

        public void UpdateDeviceUsage(DeviceUsage modified)
        {
            WorkRequestID = modified.WorkRequestID;
            WorkPlanID = modified.WorkPlanID;
            SafetyDocumentID = modified.SafetyDocumentID;
            DeviceID = modified.DeviceID;
        }
    }
}
