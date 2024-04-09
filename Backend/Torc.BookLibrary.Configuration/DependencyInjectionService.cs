using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Torc.BookLibrary.Business;
using Torc.BookLibrary.Data;

namespace Torc.BookLibrary.Configuration;

public class DependencyInjectionService
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<LibraryDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());
        services.AddSingleton(typeof(LibraryDbContext));
        services.AddTransient(typeof(MigrationService));
        services.AddTransient<IBookService, BookService>();
    }
}