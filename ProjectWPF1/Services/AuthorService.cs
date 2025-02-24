using Models;
using ProjectWPF1.Data;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWPF1.Services;

public class AuthorService
{
    private readonly DatabaseContext _databaseContext;

    public AuthorService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public List<Author> GetAllAuthors()
    {
        return _databaseContext.Authors.OrderBy(a => a.Id).ToList();
    }

    public void AddAuthor(Author author)
    {
        _databaseContext.Authors.Add(author);
        _databaseContext.SaveChanges();
    }

    public bool UpdateAuthor(int id, Author updatedAuthor)
    {
        Author? author = _databaseContext.Authors.Find(id);

        if (author == null) return false;

        author.FirstName = updatedAuthor.FirstName;
        author.LastName = updatedAuthor.LastName;
        author.BirthDate = updatedAuthor.BirthDate;
        author.Country = updatedAuthor.Country;

        _databaseContext.SaveChanges();
        return true;
    }

    public bool DeleteAuthor(int id)
    {
        Author? author = _databaseContext.Authors.Find(id);

        if (author == null) return false;

        _databaseContext.Remove(author);

        _databaseContext.SaveChanges();
        return true;
    }
}