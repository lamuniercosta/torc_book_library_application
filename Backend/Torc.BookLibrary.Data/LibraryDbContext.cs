using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Torc.BookLibrary.Data.Models;

namespace Torc.BookLibrary.Data;

public class LibraryDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public LibraryDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(
            b =>
            {
                b.Property(b => b.Title)
                    .HasMaxLength(100);
                b.Property(b => b.FirstName)
                    .HasMaxLength(50);
                b.Property(b => b.LastName)
                    .HasMaxLength(50);
                b.Property(x => x.TotalCopies)
                    .HasDefaultValue(0);
                b.Property(x => x.CopiesInUse)
                    .HasDefaultValue(0);
                b.Property(b => b.Type)
                    .HasMaxLength(50);
                b.Property(b => b.ISBN)
                    .HasMaxLength(80);
                b.Property(b => b.Category)
                    .HasMaxLength(50);
            });
        
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Title);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.FirstName);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.LastName);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Type);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN);

        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Category);
        
    }
    
    public async Task<IEnumerable<T>> SearchEntities<T>(string query, object parameters)
    {
        await using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return await connection.QueryAsync<T>(query, parameters);
    }
}