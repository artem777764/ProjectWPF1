using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Documents;

namespace Models;

public record Author
{
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public string? Country { get; set; }

    public List<Book>? Books { get; set; }
}