using Microsoft.AspNetCore.Identity;

namespace ShopApp.WebUI.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var password = configuration["Data:AdminUser:password"];
            var email = configuration["Data:AdminUser:email"];
            var role = configuration["Data:AdminUser:role"];

            if(await userManager.FindByNameAsync(username) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));

                var user = new User()
                {
                    UserName = username,
                    Email = email,
                    FirstName ="admin",
                    LastName ="admin",
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(user,password);

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
