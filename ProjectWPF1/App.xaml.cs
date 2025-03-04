﻿using ProjectWPF1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using ProjectWPF1.Services;

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
            string connectionString = "Host=localhost;Port=5432;Database=WPFDbProject1;Username=postgres;Password=12345";
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddTransient<AuthorService>();
            services.AddTransient<BookService>();
            services.AddTransient<GenreService>();

            services.AddSingleton<MainWindow>();

            services.AddTransient<AuthorWindow>();
            services.AddTransient<BookWindow>();
            services.AddTransient<GenreWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
