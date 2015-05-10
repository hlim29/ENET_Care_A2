using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENET_Care.BusinessLogic
{
    class PackageLogic
    {
        public enum Result
        {
            Ok, PastDate, NoDate, Default
        };

        public static Result ValidateInput(DateTime expiryDate)
        {
            if (expiryDate < DateTime.Now)
                return Result.PastDate;
            else
                return Result.Ok;
        }
        public static void AddPackage(Package package)
        {
            using (var context = new Entities())
            {
                context.Packages.Add(package);
                context.SaveChanges();
            }
        }

        public static int RegisterPackage(DateTime expiryDate, int medicationId)
        {
            Package p = new Package();
            p.ExpiryDate = expiryDate;
            p.PackageStandardTypeId = medicationId;
            int result = -1;
            if (ValidateInput(expiryDate) == Result.Ok)
            {
                using (var context = new Entities())
                {
                    context.Packages.Add(p);
                    context.SaveChanges();

                    return p.PackageId;
                }
            }
            return result;
            //System.Diagnostics.Debug.WriteLine(ValidateInput(expiryDate, medicationId).ToString());
        }

        public static Dictionary<int, string> GetMedicationTypes()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            using (var context = new Entities())
            {
                var query = from medication in context.PackageStandardTypes select medication;

                foreach (PackageStandardType p in query.ToList())
                {
                    result.Add(p.Id, p.Description);
                }

            }

            return result;
        }

        public static List<PackageStandardType> GetMedicationList()
        {
            using (var context = new Entities())
            {
                var query = from medication in context.PackageStandardTypes select medication;
                return query.ToList();
            }
        }

        /// <summary>
        /// Retrieves the default expiry date - the current date plus the expiry date listed on the medication
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static DateTime GetExpiryDate(int barcode)
        {
            PackageStandardType medication = GetPackageByBarcode(barcode).PackageStandardType;
            DateTime result = DateTime.Now;
            result = result.AddDays((double)medication.ShelfLife);
            return result;
        }

        public static Package GetPackageByBarcode(int barcode)
        {
            using (var context = new Entities())
            {
                var query = from package in context.Packages where package.PackageId == barcode select package;
                return query.First();
            }
            //return new Package().GetPackageByBarcode(barcode);
        }

        public static List<Package> GetPackagesByDistCentre(int centreId)
        {
            using (var context = new Entities())
            {
                var query = from package in context.PackageStatus where package.DestinationCentreID == centreId select package.Package;
                return query.ToList();
            }
            //return new Package().GetPackagesByDistCentre(centreId);
        }
    }
}
