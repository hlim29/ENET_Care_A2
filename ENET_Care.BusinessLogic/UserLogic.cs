using ENET_Care.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENET_Care.BusinessLogic
{
    public class UserLogic
    {

        public static AspNetUser GetUserById(string id)
        {
            using (var context = new  Entities())
            {
                var query = from user in context.AspNetUsers where user.Id == id select user;
                return query.First();
            }
        }

        public static DistCentre GetDistCentreByUserId(string id)
        {
            return GetUserById(id).DistCentre;
        }

        public static int GetUserDistCentreId(string id)
        {
            return (int)GetDistCentreByUserId(id).CentreId;
        }
    }
}
