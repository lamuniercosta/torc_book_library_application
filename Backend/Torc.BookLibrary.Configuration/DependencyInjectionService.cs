using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Torc.BookLibrary.Data;

namespace Torc.BookLibrary.Configuration;

public class DependencyInjectionService
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LibraryDbContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));
    }
}