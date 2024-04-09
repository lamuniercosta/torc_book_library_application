using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Torc.BookLibrary.Business;
using Torc.BookLibrary.Data;

namespace Torc.BookLibrary.Configuration;

public class DependencyInjectionService
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(typeof(LibraryDbContext));
        services.AddTransient(typeof(MigrationService));
        services.AddTransient<IBookService, BookService>();
    }
}