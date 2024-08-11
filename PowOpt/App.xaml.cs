using Microsoft.Extensions.DependencyInjection;
using PowOpt.Core.Repositories;
using PowOpt.Core.ViewModels;
using System;
using System.Windows;

namespace PowOpt
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Настройка DI
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация ViewModel и репозиториев
            services.AddSingleton<IProjectRepository, JsonProjectRepository>();
            services.AddSingleton<MainViewModel>();

            // Регистрация главного окна
            services.AddTransient<MainWindow>();
        }
    }
}
