using Microsoft.AspNetCore.Mvc;
using Torc.BookLibrary.Configuration;
using Torc.BookLibrary.Configuration.AutoMapper;

namespace Torc.BookLibrary.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient(typeof(BookController));
        DependencyInjectionService.Configure(builder.Services, builder.Configuration);
        AutoMapperService.Configure(builder.Services);
        SwaggerService.Configure(builder.Services);
        var app = builder.Build();
        
        var migrationService = (MigrationService)app.Services.GetRequiredService(typeof(MigrationService));
        migrationService.Configure();

        SwaggerService.ConfigureServices(app);

        var controller = (BookController)app.Services.GetRequiredService(typeof(BookController));
        app.MapGet("/books/search",
            new Func<string, string, Task<IResult>>(async ([FromQuery] string fieldName, string searchValue) =>
            await controller.GetBooks(fieldName, searchValue)));
        app.Run();
    }
}