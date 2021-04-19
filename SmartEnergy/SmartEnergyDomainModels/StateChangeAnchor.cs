using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class StateChangeAnchor
    {
        public int ID { get; set; }
        public int? WorkRequestID { get; set; }
        public WorkRequest WorkRequest { get; set; }
        public int? WorkPlanID { get; set; }
        public WorkPlan WorkPlan { get; set; }
        public int? SafetyDocumentID { get; set; }
        public SafetyDocument SafetyDocument { get; set; }
        public List<StateChangeHistory> StateChangeHistories { get; set; }
    }
}
