using Microsoft.EntityFrameworkCore;
using Torc.BookLibrary.Data;

namespace Torc.BookLibrary.Configuration;

public class MigrationService
{
    private readonly LibraryDbContext _context;

    public MigrationService(LibraryDbContext context)
    {
        _context = context;
    }

    public void Configure()
    {
        var seedScriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "Seed.sql");
        
        var seedScript = File.ReadAllText(seedScriptPath);
        
        _context.Database.Migrate();
        _context.Database.ExecuteSqlRaw(seedScript);
    }
}