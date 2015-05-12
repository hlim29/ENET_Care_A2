using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENET_Care.BusinessLogic
{
    public class PackageStatusLogic
    {
        /// <summary>
        /// An enum used for determining the status of a package. Synchronised with the DB, 'Status' table.
        /// DO NOT CHANGE THIS! I'm serious! >:(
        /// </summary>
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
            p.Status = (int)StatusEnum.InStock;
            using (var context = new Entities())
            {
                context.PackageStatus.Add(p);
                context.SaveChanges();
            }
        }

        public static void ReceivePackage(int packageId, string staffId)
        {
            AlterPackage(packageId, staffId, StatusEnum.Received);
        }

        public static void DiscardPackage(int packageId, string staffId)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.GetFullPath(@"..\..\..\ENET_Care\App_Data"));
        
            AlterPackage(packageId, staffId, StatusEnum.Discarded);
        }

        public static void DistributePackage(int packageId, string staffId)
        {
            AlterPackage(packageId, staffId, StatusEnum.Distributed);
        }

        /// <summary>
        /// Alters the package state. Created because receiving, discarding and distributing do essentially
        /// the same thing - find the package Id, update the staff and status of it.
        /// </summary>
        /// <param name="packageId">ID of the package</param>
        /// <param name="staffId">ID of the staff</param>
        /// <param name="status">The new package status</param>
        public static void AlterPackage(int packageId, string staffId, StatusEnum status)
        {
            using (var context = new Entities())
            {
                var query = from p in context.PackageStatus where p.PackageID == packageId select p;

                var staffCentreQuery = from s in context.AspNetUsers where s.Id == staffId select s;
                int centreId = (int)staffCentreQuery.First().CentreId;

                PackageStatus currentPackageStatus = query.First();
                currentPackageStatus.StaffID = staffId;
                currentPackageStatus.DestinationCentreID = centreId;
                currentPackageStatus.Status = (int)status;
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

        /// <summary>
        /// Retrives a list of Packages by the status parameter. This method was created to reduce repetitive code.
        /// </summary>
        /// <param name="status">The status of packages to retrieve</param>
        /// <returns>List of packages</returns>
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

         * */
    }
}
