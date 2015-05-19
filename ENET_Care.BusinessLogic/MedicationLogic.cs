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
    }
}
