using Microsoft.Extensions.DependencyInjection;

namespace Torc.BookLibrary.Configuration.AutoMapper;

public class AutoMapperService
{
    public static void Configure(IServiceCollection services)
    {
        services.AddAutoMapper(config => config.AddProfile<BookProfile>());
    }
}