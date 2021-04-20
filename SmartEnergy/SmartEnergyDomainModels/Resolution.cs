using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels
{
    public class Resolution
    {
        public int ID { get; set; }
        public Cause Cause { get; set; }
        public Subcause Subcause { get; set; }
        public ConstructionType Construction { get; set; }
        public Material Material { get; set; }

        public int IncidentID { get; set; }
        public Incident Incident { get; set; }
    }
}
