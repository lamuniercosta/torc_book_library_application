using Dapper;
using Torc.BookLibrary.Data;

namespace Torc.BookLibrary.Business;

public interface IBookService
{
    Task<IEnumerable<BookDto>?> GetBooks(string fieldName, string searchValue);
}

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookDto>?> GetBooks(string fieldName, string searchValue)
    {
        var query = CreateDynamicQuery(fieldName, searchValue);
        return await _context.SearchEntities<BookDto>(query.RawSql, query.Parameters);
    }

    private static SqlBuilder.Template CreateDynamicQuery(string fieldName, string searchValue)
    {
        var builder = new SqlBuilder()
            .Where($"{fieldName} = @SearchValue", new { searchValue });
        return builder.AddTemplate(SqlQueries.GetBooks);
    }
}