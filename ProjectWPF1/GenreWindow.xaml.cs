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
    /// Логика взаимодействия для GenreWindow.xaml
    /// </summary>
    public partial class GenreWindow : Window
    {
        private readonly GenreService _genreService;

        public GenreWindow(GenreService genreService)
        {
            _genreService = genreService;
            InitializeComponent();
            LoadGenres();
        }

        private void LoadGenres()
        {
            List<Genre> genres = _genreService.GetAllGenres();
            DataGrid_Genres.ItemsSource = genres;

            TextBox_Name.Clear();
            TextBox_Description.Clear();
            TextBox_Id.Clear();
        }

        private void Button_AddGenre_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBox_Name.Text;
            string description = TextBox_Description.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Название и Описание должны быть заполнены!");
                return;
            }

            Genre newGenre = new Genre { Name = name, Description = description };
            _genreService.AddGenre(newGenre);

            LoadGenres();
        }

        private void Button_GetGenres_Click(object sender, RoutedEventArgs e)
        {
            LoadGenres();
        }

        private void Button_UpdateGenre_Click(object sender, RoutedEventArgs e)
        {
            string name = TextBox_Name.Text;
            string description = TextBox_Description.Text;
            string idString = TextBox_Id.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(idString))
            {
                MessageBox.Show("Название, Описание и Id должны быть заполнены!");
                return;
            }

            if (!int.TryParse(idString, out int id))
            {
                MessageBox.Show("Id введён некорректно!");
                return;
            }

            Genre updateGenre = new Genre { Name = name, Description = description };
            bool successeful = _genreService.UpdateGenre(id, updateGenre);

            if (!successeful)
            {
                MessageBox.Show("Жанр с таким Id не найден!");
                return;
            }

            LoadGenres();
        }

        private void Button_DeleteGenre_Click(object sender, RoutedEventArgs e)
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

            bool successeful = _genreService.DeleteGenre(id);

            if (!successeful)
            {
                MessageBox.Show("Жанр с таким Id не найден!");
                return;
            }

            LoadGenres();
        }
    }
}
