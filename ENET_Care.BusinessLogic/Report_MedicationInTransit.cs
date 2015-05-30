using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENET_Care.Data;

namespace ENET_Care.BusinessLogic
{
    public class Report_MedicationInTransit
    {
        public PackageStandardType Medication { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DistCentre SourceCentre { get; set; }
        public DistCentre DestinationCentre { get; set; }
    }
}
