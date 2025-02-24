using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectWPF1
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        private void Button_Genre_Click(object sender, RoutedEventArgs e)
        {
            var genreWindow = _serviceProvider.GetRequiredService<GenreWindow>();
            genreWindow.Show();
        }

        private void Button_Author_Click(object sender, RoutedEventArgs e)
        {
            var authorWindow = _serviceProvider.GetRequiredService<AuthorWindow>();
            authorWindow.Show();
        }

        private void Button_Book_Click(object sender, RoutedEventArgs e)
        {
            var bookWindow = _serviceProvider.GetRequiredService<BookWindow>();
            bookWindow.Show();
        }
    }
}
