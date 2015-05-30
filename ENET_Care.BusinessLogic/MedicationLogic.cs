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

        public static PackageStandardType GetMedicationById(int id)
        {
            using (var context = new Entities())
            {
                var query = from m in context.PackageStandardTypes where m.Id == id select m;
                return query.FirstOrDefault();
            }
        }

        public static List<Report_MedicationInStock> GetMedicationsInStock()
        {
            using (var context = new Entities())
            {
                List<Package> packages = new List<Package>();
                packages = (from p in context.PackageStatus where p.Status == (int)PackageStatusLogic.StatusEnum.InStock select p.Package).ToList();
                List<Report_MedicationInStock> medications = new List<Report_MedicationInStock>();
                medications = (from package in packages
                               group package by package.PackageStandardTypeId into p
                               select new Report_MedicationInStock
                               {
                                   Medication = p.First().PackageStandardType,
                                   Quantity = p.Count(),
                                   TotalPrice = (double)p.Sum(x => x.PackageStandardType.Cost)
                               }).ToList();


                return medications;
            }
        }

        public static List<Report_MedicationInStock> GetMedicationsInStockByDistCentre(int centreId)
        {
            using (var context = new Entities())
            {
                List<Package> packages = new List<Package>();
                packages = (from p in context.PackageStatus 
                            where p.Status == (int)PackageStatusLogic.StatusEnum.InStock && p.DestinationCentreID == centreId 
                            select p.Package).ToList();
                List<Report_MedicationInStock> medications = new List<Report_MedicationInStock>();
                medications = (from package in packages
                               group package by package.PackageStandardTypeId into p
                               select new Report_MedicationInStock
                               {
                                   Medication = p.First().PackageStandardType,
                                   Quantity = p.Count(),
                                   TotalPrice = (double)p.Sum(x => x.PackageStandardType.Cost)
                               }).ToList();


                return medications;
            }
        }

        public static List<Report_MedicationInStock> GetMedicationsDistributedByDoctor(string doctorId)
        {
            using (var context = new Entities())
            {
                List<Package> packages = new List<Package>();
                packages = (from p in context.PackageStatus
                            where p.Status == (int)PackageStatusLogic.StatusEnum.Distributed && p.StaffID == doctorId
                            select p.Package).ToList();
                List<Report_MedicationInStock> medications = new List<Report_MedicationInStock>();
                medications = (from package in packages
                               group package by package.PackageStandardTypeId into p
                               select new Report_MedicationInStock
                               {
                                   Medication = p.First().PackageStandardType,
                                   Quantity = p.Count(),
                                   TotalPrice = (double)p.Sum(x => x.PackageStandardType.Cost)
                               }).ToList();


                return medications;
            }
        }

        public static double GetTotalAmountMedication(List<Report_MedicationInStock> medications)
        {
            var totalAmout = medications.Sum(m => m.TotalPrice);
            return totalAmout;
        }
    }
}
