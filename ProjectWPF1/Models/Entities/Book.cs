using System.ComponentModel.DataAnnotations;

namespace Models;

public record Book
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public int? AuthorId { get; set; }

    public int PublishYear { get; set; }

    [Required]
    [MaxLength(13)]
    [MinLength(13)]
    public string? ISBN { get; set; }

    [Required]
    public int? GenreId { get; set; }

    [Required]
    public int QuantityInStock { get; set; }

    public Author? Author { get; set; }
    public Genre? Genre { get; set; }
}