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
        /// <summary>
        /// Get specific user by id
        /// 
        /// Changes 1 : Error regarding Connection String Fixed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AspNetUser GetUserById(string id)
        {
            using (var context = new Entities())
            {
                var query = from user in context.AspNetUsers.Include("DistCentre") where user.Id == id select user;
                return query.First();
            }
        }

        public static void UpdateUserName(string id, string newFirstName, string newLastName)
        {
            using (var context = new Entities())
            {
                var query = from user in context.AspNetUsers where user.Id == id select user;
                AspNetUser currentUser = query.First();
                currentUser.FirstName = newFirstName;
                currentUser.LastName = newLastName;
                context.SaveChanges();
            }
        }

        public static void UpdateEmail(string id, string email)
        {
            using (var context = new Entities())
            {
                var query = from user in context.AspNetUsers where user.Id == id select user;
                AspNetUser currentUser = query.First();
                currentUser.Email = email;
                context.SaveChanges();
            }
        }
        public static void UpdateCentreId(string id, int centreId)
        {
            using (var context = new Entities())
            {
                var query = from user in context.AspNetUsers where user.Id == id select user;
                AspNetUser currentUser = query.First();
                currentUser.CentreId = centreId;
                context.SaveChanges();
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

        public static List<AspNetUser> GetAllDoctors()
        {
            using (var context = new Entities())
            {
                var query = from user in context.AspNetUsers
                            join f in context.AspNetUserRoles on user.Id equals f.UserId
                            join t in context.AspNetRoles on f.RoleId equals t.Id
                            where t.Name == "doctor"
                            select user;
                return query.ToList();
            }
            //return null;
        }
    }
}