using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using PraveenMatoria;
using PraveenMatoria.Database;
using PraveenMatoria.Database.Services;
using SqliteWasmHelper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
//builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddSqliteWasmDbContextFactory<ApplicationContext>(options => options.UseSqlite("Data Source=Praveen.Matoria.Sqlite3"));
builder.Services.AddDatabaseServices();

await builder.Build().RunAsync();
