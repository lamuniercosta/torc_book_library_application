namespace Torc.BookLibrary.Data;

public static class SqlQueries
{
    public const string GetBooks = """
                                   SELECT
                                   Title,
                                   CONCAT(FirstName, " ", LastName) AS Authors,
                                   Type,
                                   ISBN,
                                   Category,
                                   CONCAT(TotalCopies - CopiesInUse, '/', TotalCopies) AS AvailableCopies
                                   FROM Books
                                   /**where**/
                                   """;
}