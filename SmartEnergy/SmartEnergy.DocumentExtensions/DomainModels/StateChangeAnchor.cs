using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.DocumentExtensions.DomainModels
{
    public class StateChangeAnchor
    {
        public int ID { get; set; }
        public List<StateChangeHistory> StateChangeHistories { get; set; }
    }
}
