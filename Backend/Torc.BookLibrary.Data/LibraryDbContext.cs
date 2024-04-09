using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
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
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(
            b =>
            {
                b.HasKey(x => x.BookId);
                b.Property(x => x.Title)
                    .HasMaxLength(100);
                b.Property(x => x.FirstName)
                    .HasMaxLength(50);
                b.Property(x => x.LastName)
                    .HasMaxLength(50);
                b.Property(x => x.TotalCopies)
                    .HasDefaultValue(0);
                b.Property(x => x.CopiesInUse)
                    .HasDefaultValue(0);
                b.Property(x => x.Type)
                    .HasMaxLength(50);
                b.Property(x => x.ISBN)
                    .HasMaxLength(80);
                b.Property(propertyExpression: x => x.Category)
                    .HasMaxLength(50);

                b.HasIndex(x => x.Title);
                b.HasIndex(x => x.FirstName);
                b.HasIndex(x => x.LastName);
                b.HasIndex(x => x.Type);
                b.HasIndex(x => x.ISBN);
                b.HasIndex(x => x.Category);
            });

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Pride and Prejudice",
                    FirstName = "Jane",
                    LastName = "Austen",
                    TotalCopies = 100,
                    CopiesInUse = 80,
                    Type = "Hardcover",
                    ISBN = "1234567891",
                    Category = "Fiction"
                },
                new Book
                {
                    BookId = 2,
                    Title = "To Kill a Mockingbird",
                    FirstName = "Harper",
                    LastName = "Lee",
                    TotalCopies = 75,
                    CopiesInUse = 65,
                    Type = "Paperback",
                    ISBN = "1234567892",
                    Category = "Fiction"
                },
                new Book
                {
                    BookId = 3,
                    Title = "The Catcher in the Rye",
                    FirstName = "J.D.",
                    LastName = "Salinger",
                    TotalCopies = 50,
                    CopiesInUse = 45,
                    Type = "Hardcover",
                    ISBN = "1234567893",
                    Category = "Fiction"
                },
                new Book
                {
                    BookId = 4,
                    Title = "The Great Gatsby",
                    FirstName = "F. Scott",
                    LastName = "Fitzgerald",
                    TotalCopies = 50,
                    CopiesInUse = 22,
                    Type = "Hardcover",
                    ISBN = "1234567894",
                    Category = "Non-Fiction"
                },
                new Book
                {
                    BookId = 5,
                    Title = "The Alchemist",
                    FirstName = "Paulo",
                    LastName = "Coelho",
                    TotalCopies = 50,
                    CopiesInUse = 35,
                    Type = "Hardcover",
                    ISBN = "1234567895",
                    Category = "Biography"
                },
                new Book
                {
                    BookId = 6,
                    Title = "The Book Thief",
                    FirstName = "Markus",
                    LastName = "Zusak",
                    TotalCopies = 75,
                    CopiesInUse = 11,
                    Type = "Hardcover",
                    ISBN = "1234567896",
                    Category = "Mystery"
                },
                new Book
                {
                    BookId = 7,
                    Title = "The Chronicles of Narnia",
                    FirstName = "C.S.",
                    LastName = "Lewis",
                    TotalCopies = 100,
                    CopiesInUse = 14,
                    Type = "Paperback",
                    ISBN = "1234567897",
                    Category = "Sci-Fi"
                },
                new Book
                {
                    BookId = 8,
                    Title = "The Da Vinci Code",
                    FirstName = "Dan",
                    LastName = "Brown",
                    TotalCopies = 50,
                    CopiesInUse = 40,
                    Type = "Paperback",
                    ISBN = "1234567898",
                    Category = "Sci-Fi"
                },
                new Book
                {
                    BookId = 9,
                    Title = "The Grapes of Wrath",
                    FirstName = "John",
                    LastName = "Steinbeck",
                    TotalCopies = 50,
                    CopiesInUse = 35,
                    Type = "Hardcover",
                    ISBN = "1234567899",
                    Category = "Fiction"
                },
                new Book
                {
                    BookId = 10,
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    FirstName = "Douglas",
                    LastName = "Adams",
                    TotalCopies = 50,
                    CopiesInUse = 35,
                    Type = "Paperback",
                    ISBN = "1234567900",
                    Category = "Non-Fiction"
                },
                new Book
                {
                    BookId = 11,
                    Title = "Moby-Dick",
                    FirstName = "Herman",
                    LastName = "Melville",
                    TotalCopies = 30,
                    CopiesInUse = 8,
                    Type = "Hardcover",
                    ISBN = "8901234567",
                    Category = "Fiction"
                },
                new Book
                {
                    BookId = 12,
                    Title = "To Kill a Mockingbird",
                    FirstName = "Harper",
                    LastName = "Lee",
                    TotalCopies = 20,
                    CopiesInUse = 0,
                    Type = "Paperback",
                    ISBN = "9012345678",
                    Category = "Non-Fiction"
                },
                new Book
                {
                    BookId = 13,
                    Title = "The Catcher in the Rye",
                    FirstName = "J.D.",
                    LastName = "Salinger",
                    TotalCopies = 10,
                    CopiesInUse = 1,
                    Type = "Hardcover",
                    ISBN = "0123456789",
                    Category = "Non-Fiction"
                }
            );
    }

    public async Task<IEnumerable<T>> SearchEntities<T>(string query, object parameters)
    {
        await using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        return await connection.QueryAsync<T>(query, parameters);
    }
}