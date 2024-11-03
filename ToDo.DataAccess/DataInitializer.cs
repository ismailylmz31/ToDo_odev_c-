using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.DataAccess
{
    public class DataInitializer
    {
        public static async Task SeedRolesAndAdmin(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            // Roller yoksa oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new Role { Name = "Admin" });

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new Role { Name = "User" });

            // Admin kullanıcıyı oluştur
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var adminUser = new User { UserName = "admin", Email = "admin@todoapp.com" };
                await userManager.CreateAsync(adminUser, "Admin123!");

                // Admin rolünü ekle
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
