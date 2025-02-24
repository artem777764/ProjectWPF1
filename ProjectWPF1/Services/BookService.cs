using Microsoft.EntityFrameworkCore;
using Models;
using ProjectWPF1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF1.Services;

public class BookService
{
    private readonly DatabaseContext _databaseContext;

    public BookService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public List<Book> GetAllBooks()
    {
        return _databaseContext.Books.Include(b => b.Genre).Include(b => b.Author).OrderBy(b => b.Id).ToList();
    }

    public bool AddBook(int authorId, int genreId, Book book)
    {
        Author? author = _databaseContext.Authors.Find(authorId);
        if (author == null) return false;

        Genre? genre = _databaseContext.Genres.Find(genreId);
        if (genre == null) return false;

        _databaseContext.Books.Add(book);
        _databaseContext.SaveChanges();

        return true;
    }

    public bool UpdateBook(int id, int authorId, int genreId, Book updatedBook)
    {
        Author? author = _databaseContext.Authors.Find(authorId);
        if (author == null) return false;

        Genre? genre = _databaseContext.Genres.Find(genreId);
        if (genre == null) return false;

        Book? book = _databaseContext.Books.Find(id);
        if (book == null) return false;

        book.Title = updatedBook.Title;
        book.AuthorId = updatedBook.AuthorId;
        book.PublishYear = updatedBook.PublishYear;
        book.ISBN = updatedBook.ISBN;
        book.GenreId = updatedBook.GenreId;

        _databaseContext.SaveChanges();
        return true;
    }

    public bool DeleteBook(int id)
    {
        Book? book = _databaseContext.Books.Find(id);

        if (book == null) return false;

        _databaseContext.Remove(book);

        _databaseContext.SaveChanges();
        return true;
    }
}
