using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace PruebaTecnica.Data.Seed
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                string email = "juan.morales@gmail.com";
                string pasword = "JuanMorales123*";
                string id = "e16bfd7a-a24b-40c6-92d4-dbddb739fb47";

                if (roleName == "Client")
                {
                    id = "1a430797-318f-4961-8b71-ba880a928941";
                    email = "daniel.poveda@gmail.com";
                    pasword = "DanielPoveda123*";
                }

                await CreateUsers(email, pasword, roleName, userManager, id);
            }
        }

        private static async Task CreateUsers(string email, string password, string rol, UserManager<IdentityUser> userManager, string id)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                var adminUser = new IdentityUser
                {
                    Id = id,
                    UserName = email,
                    Email = email
                };

                var createAdminUser = await userManager.CreateAsync(adminUser, password);
                if (createAdminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, rol);
                }
            }
        }
    }
}
