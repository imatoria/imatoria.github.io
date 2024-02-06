using PraveenMatoria.Database;
using SqliteWasmHelper;

namespace PraveenMatoria.Database.Services
{
    public class TodoService(ISqliteWasmDbContextFactory<ApplicationContext> db)
    {
        public async Task<List<Todo>> GetAllAsync()
        {
            using var dbContext = await db.CreateDbContextAsync();
            if (!dbContext.Todos.Any())
            {
                dbContext.Todos.AddRange(new List<Todo>()
                {
                    new(1, "Wash Clothes"),
                    new(2, "Clean Desk", false)
                });
                await dbContext.SaveChangesAsync();
            }

            return [.. dbContext.Todos];
        }

        public async void Add(Todo item)
        {
            using var dbContext = await db.CreateDbContextAsync();
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Todo item)
        {
            using var dbContext = await db.CreateDbContextAsync();
            var todo = dbContext.Todos.FirstOrDefault(x => x.Id == item.Id);
            if (todo == null) return;

            todo.Name = item.Name;
            todo.IsPersonal = item.IsPersonal;
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Todo item)
        {
            using var dbContext = await db.CreateDbContextAsync();
            dbContext.Todos.Remove(item);
            await dbContext.SaveChangesAsync();
        }
        public async Task ToggleCompleteAsync(Todo item)
        {
            using var dbContext = await db.CreateDbContextAsync();
            var todo = dbContext.Todos.FirstOrDefault(x => x.Id == item.Id);
            if (todo == null) return;

            todo.IsComplete = !todo.IsComplete;
            await dbContext.SaveChangesAsync();
        }
    }
}
