using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Eventures.Web.Utilities
{
    public static class Seeder
    {
        public static void Seed(IServiceProvider provider)
        {
            var roleManager = provider.GetService<RoleManager<IdentityRole>>();
            var adminRoleExists = roleManager.RoleExistsAsync("Administrator").GetAwaiter().GetResult();
            if (!adminRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole("Administrator")).GetAwaiter().GetResult();
            }
            var userRoleExists = roleManager.RoleExistsAsync("User").GetAwaiter().GetResult();
            if (!userRoleExists)
            {
                roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
            }
        }
    }
}