using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VNH.BE.Domain.Aggregates.Identity;
using VNH.BE.Infrastructure;

namespace VNH.BE.API.Infrastructure
{
    public static class DataSeeder
    {
        public static void SeedUser(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var listUser = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        Id = "00000000-0000-0000-0000-000000000000",
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        Email = "admin@gmail.com",
                        // Password: Admin@123
                        PasswordHash = "AQAAAAEAACcQAAAAEO1bJSt5I2nVl3OOK8+JxA6lxF5F10TCCQ819Bm7ySPdVliVt4Vv54OouSrd4EcWbQ==",
                        IsAdmin = true,
                        IsActive = true,
                    },
                    new ApplicationUser
                    {
                        Id = "11111111-1111-1111-1111-111111111111",
                        UserName = "system",
                        NormalizedUserName = "SYSTEM",
                        Email = "system@gmail.com",
                        // Password: Admin@123
                        PasswordHash = "AQAAAAEAACcQAAAAEO1bJSt5I2nVl3OOK8+JxA6lxF5F10TCCQ819Bm7ySPdVliVt4Vv54OouSrd4EcWbQ==",
                        IsAdmin = true,
                        IsActive = true,
                    }
                };
                context.Users.AddRange(listUser);
                context.SaveChanges();
            }
        }
    }
}
