using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENET_Care.BusinessLogic
{
    public class PackageLogic
    {
        public enum Result
        {
            Ok, PastDate, NoDate, Default
        };

        public static Result ValidateInput(DateTime expiryDate)
        {
            if (expiryDate < DateTime.Now)
                return Result.PastDate;
            else if (expiryDate > DateTime.Now)
                return Result.Ok;
            else
                return Result.NoDate;
        }
        public static void AddPackage(Package package)
        {
            using (var context = new Entities())
            {
                context.Packages.Add(package);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Registers the package to the system and returns the barcode
        /// </summary>
        /// <param name="expiryDate">Expiry date of the package</param>
        /// <param name="medicationId">The medication ID</param>
        /// <returns>Barcode</returns>
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

        /// <summary>
        /// Returns a list of all package standard types, i.e. medications
        /// </summary>
        /// <returns>List</returns>
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

        /// <summary>
        /// Returns the package which barcode matches with the parameter
        /// </summary>
        /// <param name="barcode">The barcode of the package</param>
        /// <returns>The package</returns>
        public static Package GetPackageByBarcode(int barcode)
        {
            using (var context = new Entities())
            {
                var query = from package in context.Packages where package.PackageId == barcode select package;
                return query.First();
            }
        }

        public static List<Package> GetPackagesByDistCentre(int centreId)
        {
            using (var context = new Entities())
            {
                var query = from package in context.PackageStatus where package.DestinationCentreID == centreId select package.Package;
                return query.ToList();
            }
        }
    }
}
