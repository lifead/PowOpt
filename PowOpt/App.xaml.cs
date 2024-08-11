using Microsoft.Extensions.DependencyInjection;
using PowOpt.Core.Repositories;
using PowOpt.Core.Services;
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

            // Создание и показ главного окна через DI
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Регистрация зависимостей
            services.AddSingleton<IProjectRepository, JsonProjectRepository>();
            services.AddSingleton<DataTransformationService>(); // Регистрация DataTransformationService
            services.AddSingleton<MainViewModel>();

            // Регистрация главного окна
            services.AddTransient<MainWindow>(provider =>
            {
                var viewModel = provider.GetRequiredService<MainViewModel>();
                return new MainWindow
                {
                    DataContext = viewModel
                };
            });
        }
    }
}
