using kredit.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kredit.Startup))]
namespace kredit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoleandUsers();
        }

        private void createRoleandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //membuat admin role pertama di startup dan membuat default admin user
            if (!roleManager.RoleExists("Admin"))
            {
                //membuat role admin
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //membuat user dengan role admin
                var user = new ApplicationUser();
                user.UserName = "hafidzzakkyd";
                user.Email = "hafidzzakkyd@gmail.com";
                user.EmailConfirmed = true;
                string userPWD = "Admin@2018";

                var chkUser = userManager.Create(user, userPWD);

                //tambah user admin
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

                //membuat user dengan role User
                var user = new ApplicationUser();
                user.UserName = "farishaidar";
                user.Email = "farishaidar@gmail.com";
                user.EmailConfirmed = true;
                string userPWD = "User@2018";

                var chkUser = userManager.Create(user, userPWD);

                //tambah user admin
                if (chkUser.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "User");
                }
            }
        }
    }
}
