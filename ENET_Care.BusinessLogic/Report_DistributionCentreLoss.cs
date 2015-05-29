using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENET_Care.Data;

namespace ENET_Care.BusinessLogic
{
    public class Report_DistributionCentreLoss
    {
        public double LossRatio { get; set; }
        public double TotalValueLost { get; set; }
        public DistCentre DistCentre { get; set; }
    }
}
