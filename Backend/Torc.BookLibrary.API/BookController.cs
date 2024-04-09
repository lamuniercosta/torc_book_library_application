using Torc.BookLibrary.Business;

namespace Torc.BookLibrary.API;

public class BookController
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }
    
    public async Task<IResult> GetBooks(string fieldName, string searchValue)
    {
        var result = await _service.GetBooks(fieldName, searchValue);

        if (result != null)
        {
            return TypedResults.Ok(result);
        }

        return TypedResults.NotFound();
    }
}