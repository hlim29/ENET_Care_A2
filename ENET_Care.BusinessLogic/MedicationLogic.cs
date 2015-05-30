 using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENET_Care.BusinessLogic
{
    public class MedicationLogic
    {
        public static List<PackageStandardType> GetAllMedications()
        {
            using (var context = new Entities())
            {
                var query = from m in context.PackageStandardTypes select m;
                return query.ToList();
            }
        }

        public static List<Report_MedicationInStock> GetMedicationsInStock()
        {
            using (var context = new Entities())
            {
                List<Package> packages = PackageStatusLogic.GetPackagesListByStatus(PackageStatusLogic.StatusEnum.InStock);
                List<Report_MedicationInStock> medications = new List<Report_MedicationInStock>();
                medications = packages.
                              GroupBy(l => l.PackageStandardTypeId).
                              Select(x => new Report_MedicationInStock
                              {
                                  Medication = x.First().PackageStandardType,
                                  Quantity = x.Count(),
                                  TotalPrice = (double)x.Sum(p => p.PackageStandardType.Cost)
                              }).ToList();


                return medications;
            }
        }

        public static double GetTotalAmountMedicationInStock(List<Report_MedicationInStock> medications)
        {
            var totalAmout = medications.Sum(m => m.TotalPrice);
            return totalAmout;
        }
    }
}
