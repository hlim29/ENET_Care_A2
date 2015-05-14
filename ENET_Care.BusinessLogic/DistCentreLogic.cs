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
    }
}
