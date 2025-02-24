using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models;

public record Genre
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public List<Book>? Books { get; set; }
}