using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergyDomainModels.Enums
{
    public enum CallReason
    {
        NO_POWER,
        FLICKERING_LIGHT,
        PARTIAL_POWER,
        VOLTAGE_PROBLEM,
        POWER_RESTORED,
        MALFUNCTION
    }
}
