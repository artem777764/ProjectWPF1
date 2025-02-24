using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

namespace ProjectWPF1
{
    /// <summary>
    /// Логика взаимодействия для AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window
    {
        private readonly AuthorService _authorService;

        public AuthorWindow(AuthorService authorService)
        {
            _authorService = authorService;
            InitializeComponent();
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            List<Author> authors = _authorService.GetAllAuthors();
            DataGrid_Authors.ItemsSource = authors;

            TextBox_FirstName.Clear();
            TextBox_LastName.Clear();
            TextBox_Birthday.Clear();
            TextBox_Country.Clear();
            TextBox_Id.Clear();
        }

        private void Button_AddAuthor_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TextBox_FirstName.Text;
            string lastName = TextBox_LastName.Text;
            string birthday = TextBox_Birthday.Text;
            string country = TextBox_Country.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                MessageBox.Show("Имя и Фамилия должны быть заполнены!");
                return;
            }

            if (!DateTime.TryParse(birthday, out DateTime birthDate))
            {
                MessageBox.Show("Некорректный формат даты!");
                return;
            }

            birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

            Author newAuthor = new Author { FirstName = firstName, LastName = lastName, BirthDate = birthDate, Country = country };
            _authorService.AddAuthor(newAuthor);

            LoadAuthors();
        }

        private void Button_UpdateAuthor_Click(object sender, RoutedEventArgs e)
        {
            string firstName = TextBox_FirstName.Text;
            string lastName = TextBox_LastName.Text;
            string birthday = TextBox_Birthday.Text;
            string country = TextBox_Country.Text;
            string idString = TextBox_Id.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(idString))
            {
                MessageBox.Show("Имя, Фамилия и Id должны быть заполнены!");
                return;
            }

            if (!DateTime.TryParse(birthday, out DateTime birthDate))
            {
                MessageBox.Show("Некорректный формат даты!");
                return;
            }

            birthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);

            if (!int.TryParse(idString, out int id))
            {
                MessageBox.Show("Id введён некорректно!");
                return;
            }

            Author updateAuthor = new Author { FirstName = firstName, LastName = lastName, BirthDate = birthDate, Country = country };
            bool successeful = _authorService.UpdateAuthor(id, updateAuthor);

            if (!successeful)
            {
                MessageBox.Show("Жанр с таким Id не найден!");
                return;
            }

            LoadAuthors();
        }

        private void Button_DeleteAuthor_Click(object sender, RoutedEventArgs e)
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

            bool successeful = _authorService.DeleteAuthor(id);

            if (!successeful)
            {
                MessageBox.Show("Жанр с таким Id не найден!");
                return;
            }

            LoadAuthors();
        }

        private void Button_GetAuthors_Click(object sender, RoutedEventArgs e)
        {
            LoadAuthors();
        }
    }
}
