using Models;
using ProjectWPF1.Data;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWPF1.Services;

public class GenreService
{
    private readonly DatabaseContext _databaseContext;

    public GenreService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public List<Genre> GetAllGenres()
    {
        return _databaseContext.Genres.OrderBy(g => g.Id).ToList();
    }

    public void AddGenre(Genre genre)
    {
        _databaseContext.Add(genre);
        _databaseContext.SaveChanges();
    }

    public bool UpdateGenre(int id, Genre updatedGenre)
    {
        Genre? genre = _databaseContext.Genres.Find(id);

        if (genre == null) return false;

        genre.Name = updatedGenre.Name;
        genre.Description = updatedGenre.Description;

        _databaseContext.SaveChanges();
        return true;
    }

    public bool DeleteGenre(int id)
    {
        Genre? genre = _databaseContext.Genres.Find(id);

        if (genre == null) return false;

        _databaseContext.Remove(genre);

        _databaseContext.SaveChanges();
        return true;
    }
}