using AutoMapper;
using Torc.BookLibrary.Business;
using Torc.BookLibrary.Data.Models;

namespace Torc.BookLibrary.Configuration.AutoMapper;

public class BookProfile : Profile
{
    public BookProfile() : base(nameof(BookProfile))
    {
        CreateMap<Book, BookDto>();
    }
    
}