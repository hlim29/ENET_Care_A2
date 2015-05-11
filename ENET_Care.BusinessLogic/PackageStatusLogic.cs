using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENET_Care.BusinessLogic
{
    class PackageStatusLogic
    {
        public enum StatusEnum
        {
            InTransit = 1,
            InStock = 2,
            Lost = 3,
            Discarded = 4,
            Distributed = 5,
            Received = 6
        };
        public static void RegisterArrival(int packageId, int centreId, string staffId)
        {
            PackageStatus p = new PackageStatus();
            p.PackageID = packageId;
            p.DestinationCentreID = centreId;
            p.StaffID = staffId;
            using (var context = new Entities())
            {
                context.PackageStatus.Add(p);
                context.SaveChanges();
            }
        }

        public static void ReceivePackage(int packageId, string staffId)
        {
            using (var context = new Entities())
            {
                var query = from p in context.PackageStatus where p.PackageID == packageId select p;

                var staffCentreQuery = from s in context.AspNetUsers where s.Id == staffId select s;
                int centreId = (int)staffCentreQuery.First().CentreId;

                PackageStatus currentPackageStatus = query.First();
                currentPackageStatus.StaffID = staffId;
                currentPackageStatus.DestinationCentreID = centreId;
                currentPackageStatus.Status = (int)StatusEnum.InStock;
                context.SaveChanges();
            }
            //new PackageStatus().ReceivePackage(packageId, staffId);
        }

        public static void DistributePackage(int packageId, string staffId)
        {
            using (var context = new Entities())
            {
                var query = from p in context.PackageStatus where p.PackageID == packageId select p;

                var staffCentreQuery = from s in context.AspNetUsers where s.Id == staffId select s;
                int centreId = (int)staffCentreQuery.First().CentreId;

                PackageStatus currentPackageStatus = query.First();
                currentPackageStatus.StaffID = staffId;
                currentPackageStatus.DestinationCentreID = centreId;
                currentPackageStatus.Status = (int)StatusEnum.Distributed;
                context.SaveChanges();
            }
        }

        public static void SendPackage(int source, int destination, string staffId, int packageId)
        {
            using (var context = new Entities())
            {
                var query = from p in context.PackageStatus where p.PackageID == packageId select p;

                var staffCentreQuery = from s in context.AspNetUsers where s.Id == staffId select s;
                int centreId = (int)staffCentreQuery.First().CentreId;

                PackageStatus currentPackageStatus = query.First();
                currentPackageStatus.StaffID = staffId;
                currentPackageStatus.SourceCentreID = centreId;
                currentPackageStatus.DestinationCentreID = destination;
                currentPackageStatus.Status = (int)StatusEnum.InTransit;
            }
        }

        public static PackageStatus GetPackageStatusById(int packageId)
        {
            using (var context = new Entities())
            {
                var query = from p in context.PackageStatus where p.PackageID == packageId select p;
                return query.First();
            }
        }

        public static List<Package> GetAllLostPackages()
        {
            return GetPackagesListByStatus(StatusEnum.Lost);
        }

        public static List<Package> GetInTransitPackages()
        {
            return GetPackagesListByStatus(StatusEnum.InTransit);
        }

        public static List<Package> GetPackagesListByStatus(StatusEnum status)
        {
            using (var context = new Entities())
            {
                var query = from p in context.PackageStatus where p.Status == (int)status select p.Package;
                return query.ToList();
            }
        }

        /*
        public static bool HasStatus(int packageId)
        {
            return (new PackageStatus().CountPackageById(packageId) == 1);
        }

        

        //Auto-Generated Method

        public static void DiscardPackageStatus(int centreId, int barcode, string staffId)
        {
            new PackageStatus().UpdatePackageStatusByBarcodeAndCentreId((int)StatusEnum.Discard, staffId, centreId, barcode);

        }

        public static bool IsPackageInStock(int barcode)
        {
            PackageStatus pS = new PackageStatus();
            pS.SetPackageByBarCode(barcode);
            if ((int)pS.Status == (int)StatusEnum.InStock || (int)pS.Status == (int)StatusEnum.Received)
            {
                return true;
            }
            return false;
        }

        public static Dictionary<int, string> GetAllPackgesLost()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (Package packages in new PackageStatus().GetAllPackagesByStatus((int)StatusEnum.Lost))
            {
                result.Add(packages.BarCode, packages.BarCode + " - " + packages.Medication.Description);
            }
            return result;
        }

        public static System.Data.DataSet GetAllLostPackages()
        {
            return new PackageStatus().GetLostPackages();
        }

        public static System.Data.DataSet GetInTransitPackages()
        {
            return new PackageStatus().GetInTransitPackages();
        }
         * */
    }
}
