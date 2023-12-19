using Microsoft.EntityFrameworkCore;
using TodoProject.Models;
using TodoProject.Utils;

namespace TodoProject.Data
{
    public class SeedDataTodo
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

            List<Todo> todoList = new();
            for (int i = 1; i <= 100; i++)
            {
                todoList.Add(new Todo
                {
                    UserProfileId = Random.Shared.Next(1,6),
                    Description = $"Todo item {i}",
                    IsComplete = Helpers.GetRandomBool(),
                    ModifiedBy = "SEED",
                    ModifiedDate = Helpers.GetRandomDate(),
                });
            }

            context.Todos.AddRange(todoList);
            context.SaveChanges();
        }
    }
}
