using Models;
using ProjectWPF1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ProjectWPF1
{
    /// <summary>
    /// Логика взаимодействия для BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        private readonly AuthorService _authorService;
        private readonly BookService _bookService;
        private readonly GenreService _genreService;

        public BookWindow(AuthorService authorService, BookService bookService, GenreService genreService)
        {
            _authorService = authorService;
            _bookService = bookService;
            _genreService = genreService;

            InitializeComponent();

            LoadAuthors();
            LoadBooks();
            LoadGenres();
        }

        private void LoadAuthors()
        {
            List<Author> authors = _authorService.GetAllAuthors();
            ComboBox_Authors.ItemsSource = authors;
        }

        private void LoadBooks()
        {
            int genreId = 0;
            if (ComboBox_Genres.SelectedValue != null) genreId = (int)ComboBox_Genres.SelectedValue;

            int authorId = 0;
            if (ComboBox_Authors.SelectedValue != null) authorId = (int)ComboBox_Authors.SelectedValue;

            string search = TextBox_Search.Text;

            List<Book> books = _bookService.GetAllBooks();

            if (genreId != 0 ) books = books.Where(b => b.GenreId == genreId).ToList();
            if (authorId != 0 ) books = books.Where(b => b.AuthorId == authorId).ToList();
            if (!String.IsNullOrWhiteSpace(search)) books = books.Where(b => b.Title!.StartsWith(search)).ToList();

            DataGrid_Books.ItemsSource = books;

            Clear();
        }

        private void LoadGenres()
        {
            List<Genre> genres = _genreService.GetAllGenres();
            ComboBox_Genres.ItemsSource = genres;
        }

        private void Clear()
        {
            TextBox_Name.Clear();
            TextBox_AuthorId.Clear();
            TextBox_PublishYear.Clear();
            TextBox_ISBN.Clear();
            TextBox_GenreId.Clear();
            TextBox_QuantityInStock.Clear();
            TextBox_Id.Clear();
        }

        private void Button_GetBooks_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void Button_AddBook_Click(object sender, RoutedEventArgs e)
        {
            string title = TextBox_Name.Text;
            string authorIdString = TextBox_AuthorId.Text;
            string publishYearString = TextBox_PublishYear.Text;
            string iSBN = TextBox_ISBN.Text;
            string genreIdString = TextBox_GenreId.Text;
            string quantityString = TextBox_QuantityInStock.Text;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(authorIdString)
                || string.IsNullOrWhiteSpace(iSBN) || string.IsNullOrWhiteSpace(genreIdString)
                || string.IsNullOrWhiteSpace(quantityString))
            {
                MessageBox.Show("Название, Id Автора, ISBN, Id Жанра и Количество должны быть заполнены!");
                return;
            }

            if (!int.TryParse(authorIdString, out int authorId))
            {
                MessageBox.Show("Id Автора введён некорректно!");
                return;
            }

            if (!int.TryParse(genreIdString, out int genreId))
            {
                MessageBox.Show("Id Жанра введён некорректно!");
                return;
            }

            if (!int.TryParse(quantityString, out int quantity))
            {
                MessageBox.Show("Количество введёно некорректно!");
                return;
            }

            Book book = new Book
            {
                Title = title,
                AuthorId = authorId,
                ISBN = iSBN,
                GenreId = genreId,
                QuantityInStock = quantity
            };
            if (int.TryParse(publishYearString, out int publishYear) && !String.IsNullOrWhiteSpace(publishYearString)) book.PublishYear = publishYear;
            bool successful = _bookService.AddBook(authorId, genreId, book);

            if (!successful)
            {
                MessageBox.Show("Автор или жанр не найдены!");
                return;
            }

            LoadBooks();
        }

        private void Button_UpdateBook_Click(object sender, RoutedEventArgs e)
        {
            string title = TextBox_Name.Text;
            string authorIdString = TextBox_AuthorId.Text;
            string publishYearString = TextBox_PublishYear.Text;
            string iSBN = TextBox_ISBN.Text;
            string genreIdString = TextBox_GenreId.Text;
            string quantityString = TextBox_QuantityInStock.Text;
            string idString = TextBox_Id.Text;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(authorIdString)
                || string.IsNullOrWhiteSpace(iSBN) || string.IsNullOrWhiteSpace(genreIdString)
                || string.IsNullOrWhiteSpace(quantityString) || string.IsNullOrWhiteSpace(idString))
            {
                MessageBox.Show("Название, Id Автора, Год Публикации, ISBN, Id Жанра, Количество и Id должны быть заполнены!");
                return;
            }

            if (!int.TryParse(authorIdString, out int authorId))
            {
                MessageBox.Show("Id Автора введён некорректно!");
                return;
            }

            if (!int.TryParse(genreIdString, out int genreId))
            {
                MessageBox.Show("Id Жанра введён некорректно!");
                return;
            }

            if (!int.TryParse(quantityString, out int quantity))
            {
                MessageBox.Show("Количество введёно некорректно!");
                return;
            }

            if (!int.TryParse(idString, out int id))
            {
                MessageBox.Show("Количество введёно некорректно!");
                return;
            }

            Book book = new Book
            {
                Title = title,
                AuthorId = authorId,
                ISBN = iSBN,
                GenreId = genreId,
                QuantityInStock = quantity
            };
            if (int.TryParse(publishYearString, out int publishYear) && !String.IsNullOrWhiteSpace(publishYearString)) book.PublishYear = publishYear;
            bool successful = _bookService.UpdateBook(id, authorId, genreId, book);

            if (!successful)
            {
                MessageBox.Show("Автор, Жанр или Книга не найдены!");
                return;
            }

            LoadBooks();
        }

        private void Button_DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            string idString = TextBox_Id.Text;

            if (string.IsNullOrWhiteSpace(idString))
            {
                MessageBox.Show("Id должен быть заполнен!");
                return;
            }

            if (!int.TryParse(idString, out int id))
            {
                MessageBox.Show("Id введён некорректно!");
                return;
            }

            bool successeful = _bookService.DeleteBook(id);

            if (!successeful)
            {
                MessageBox.Show("Книга с таким Id не найдена!");
                return;
            }

            LoadBooks();
        }
    }
}
