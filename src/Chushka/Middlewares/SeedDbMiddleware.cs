using System;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Eventures.App.Middlewares
{
    public class SeedDbMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDbMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IServiceProvider provider)
        {
            var roleManager = provider.GetService<RoleManager<IdentityRole>>();
            var userManager = provider.GetService<UserManager<EventuresUser>>();

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

            if (!userManager.Users.Any())
            {
                var user = new EventuresUser
                {
                    UserName = "admin",
                    Email = "admin@eventures.com",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    UniqueCitizenNumber = "1234567890"
                };

                var result = userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }

            await this.next(httpContext);
        }
    }
}