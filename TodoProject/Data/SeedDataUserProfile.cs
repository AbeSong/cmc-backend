using Microsoft.EntityFrameworkCore;
using TodoProject.Models;
using TodoProject.Utils;

namespace TodoProject.Data
{
    public class SeedDataUserProfile
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
            if (context.Todos.Any())
            {
                return;   // DB has been seeded
            }

            List<UserProfile> list = new();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(new UserProfile
                {
                    RoleId = Random.Shared.Next(1,3),
                    UserName = $"UserName_{i}",
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Email = $"person{i}@example.com",
                    ModifiedBy = "SEED",
                    ModifiedDate = Helpers.GetRandomDate(),
                });
            }

            context.UserProfiles.AddRange(list);
            context.SaveChanges();
        }
    }
}
