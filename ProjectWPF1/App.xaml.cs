using ProjectWPF1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ProjectWPF1
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Подключение к PostgreSQL
            string connectionString = "Host=localhost;Port=5432;Database=WPFDbProject1;Username=postgres;Password=12345";
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(connectionString));

            // Регистрация CRUD-сервиса
            //services.AddScoped<ProductService>();

            // Регистрация главного окна
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
