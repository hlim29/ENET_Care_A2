using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENET_Care.Data;

namespace ENET_Care.BusinessLogic
{
    public static class UserLogic
    {
        public static int GetUserCentre(string id)
        {
            return (int)GetUserById(id).CentreId;
        }

        public static AspNetUser GetUserById(string id)
        {
            using (var context = new Entities())
            {
                var query = from user in context.AspNetUsers where user.Id == id select user;
                return query.First();
            }
        }

        public static void ChangeUserPassword(string id, string oldPassword, string newPassword)
        {

        }
    }
}
