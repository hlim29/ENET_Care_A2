using ENET_Care.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ENET_Care.Startup))]
namespace ENET_Care
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));


            if (!roleManager.RoleExists("manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "manager";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("agent"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "agent";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("doctor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "doctor";
                roleManager.Create(role);
            }

            using (var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())))
            using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                var user = um.FindByEmail("agent@enetcare.org");
                um.AddToRole(user.Id, "agent");

                user = um.FindByEmail("manager@enetcare.org");
                um.AddToRole(user.Id, "manager");

                user = um.FindByEmail("doctor@enetcare.org");
                um.AddToRole(user.Id, "doctor");
            }
        }
    }
}
