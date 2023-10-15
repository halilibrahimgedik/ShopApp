using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ShopApp.WebUI.Identity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICartService cartService, IConfiguration configuration)
        {
            var roles = configuration.GetSection("Data:Roles").GetChildren().Select(r => r.Value).ToArray();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role)) // gelen role db de yoksa ekle
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var users = configuration.GetSection("Data:Users");

            foreach (var section in users.GetChildren())
            {
                var username = section.GetValue<string>("username");
                var password = section.GetValue<string>("password");
                var email = section.GetValue<string>("email");
                var role = section.GetValue<string>("role");
                var firstname = section.GetValue<string>("firstname");
                var lastname = section.GetValue<string>("lastname");


                if (await userManager.FindByNameAsync(username) == null)
                {
                    var user = new User()
                    {
                        UserName = username,
                        Email = email,
                        FirstName = firstname,
                        LastName = lastname,
                        EmailConfirmed = true,
                    };

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                        cartService.InitializeCart(user.Id);
                    }
                }
            }

        }
    }
}
