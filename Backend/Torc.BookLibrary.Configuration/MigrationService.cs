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
        _context.Database.Migrate();
    }
}