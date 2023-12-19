using Microsoft.EntityFrameworkCore;
using TodoProject.Models;

namespace TodoProject.Data
{
    public class SeedDataRole
    {
        public static Task Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SimpleDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<SimpleDbContext>>());
            SeedDB(context);
            return Task.CompletedTask;
        }

        public static void SeedDB(SimpleDbContext context)
        {
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            context.Roles.AddRange(
                new Role { RoleName = "Admin" },
                new Role { RoleName = "PowerUser" },
                new Role { RoleName = "RegisteredUser" }
            );

            context.SaveChanges();
        }
    }
}
