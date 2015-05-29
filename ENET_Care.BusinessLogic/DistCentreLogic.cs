using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENET_Care.Data;

namespace ENET_Care.BusinessLogic
{
    public class DistCentreLogic
    {
        /// <summary>
        /// Get All Distribution centre and put in the list
        /// Currently give exception :
        /// $exception	{"The underlying provider failed on Open."}	System.Exception {System.Data.Entity.Core.EntityException}
        /// 
        /// Debug in DistCentreTest -> DistCentreLogicTest_GetAllDistCentre_CountEqualsThree()
        /// </summary>
        /// <returns></returns>
        public static ICollection<DistCentre> GetAllDistCentre()
        {
            using(var context = new Entities())
            {
                var distCentre = from d in context.DistCentres
                                  select d;
                return distCentre.ToList();
            }
        }

        public static DistCentre GetDistCentreById(int distCentreId)
        {
            using (var context = new Entities())
            {
                var distCentre = from d in context.DistCentres where d.CentreId == distCentreId
                                 select d;
                return distCentre.FirstOrDefault();
            }
        }

        public static List<Report_DistributionCentreLoss> GetDistributionCentreLosses()
        {
            List<Report_DistributionCentreLoss> centresLosses = new List<Report_DistributionCentreLoss>();
            List<PackageStatus> packagesStatus = PackageStatusLogic.GetPackagesLostAndDiscarded();
            using (var context = new Entities())
            {
                centresLosses = packagesStatus.
                             GroupBy(l => l.DestinationCentreID).
                             Select(x => new Report_DistributionCentreLoss
                             {
                                 DistCentre = GetDistCentreById((int)x.First().DestinationCentreID),
                                 LossRatio = x.Count() / (x.Count() + PackageStatusLogic.GetPackagesStatusByStatusAndDistCentre(PackageStatusLogic.StatusEnum.Distributed, (int)x.First().DestinationCentreID).Count()),
                                 TotalValueLost = (double)x.Sum(p => p.Package.PackageStandardType.Cost)
                             }).ToList();
            }
            
            return centresLosses;
        }

    }
}
