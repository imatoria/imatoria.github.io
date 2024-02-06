using PraveenMatoria.Database.Services;

namespace PraveenMatoria.Database
{
    public static class DatabaseService
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddSingleton<TodoService>();

            return services;
        }
    }
}
