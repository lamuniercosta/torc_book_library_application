using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Torc.BookLibrary.Data.Models;

[Table("Books")]
public class Book
{
    [Key]
    public int BookId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public int TotalCopies { get; set; }
    [Required]
    public int CopiesInUse { get; set; }
    public string Type { get; set; }
    public string ISBN { get; set; }
    public string Category { get; set; }
}