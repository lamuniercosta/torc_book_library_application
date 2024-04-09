namespace Torc.BookLibrary.Data;

public static class SqlQueries
{
    public const string GetBooks = """
                                   SELECT
                                   Title,
                                   Publisher,
                                   CONCAT(FirstName, " ", LastName) AS Authors,
                                   Type,
                                   ISBN,
                                   Category,
                                   CONCAT(TotalCopies - CopiesInUse, '/', TotalCopies) AS AvailableCopies
                                   FROM BOOKS
                                   /**where**/
                                   """;
}