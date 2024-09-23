using PowOpt.Core.Repositories;  // Пространство имен, где находятся ваши репозитории
using PowOpt.Core.Services;
using PowOpt.Core.ViewModels;  // Пространство имен, где находится ваш MainWindow
using PowOpt.Services;
using System.Windows;

namespace PowOpt
{
    public partial class App : PrismApplication
    {
        // Метод CreateShell отвечает за создание главного окна
        protected override Window CreateShell()
        {
            try
            {
                var mainWindow = Container.Resolve<MainWindow>();
                mainWindow.DataContext = Container.Resolve<MainViewModel>();
                return mainWindow;
            }
            catch (Exception ex)
            {
                // Логирование или отображение ошибки
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Регистрация зависимостей
            containerRegistry.RegisterSingleton<IProjectRepository, JsonProjectRepository>();
            containerRegistry.RegisterSingleton<DataTransformationService>();
            containerRegistry.RegisterSingleton<IWindowService, WindowService>();
            containerRegistry.RegisterSingleton<MainViewModel>();
            containerRegistry.RegisterSingleton<EditMatrixViewModel>();

            // Регистрация главного окна (если вам нужно передать DataContext через DI)
            containerRegistry.Register<MainWindow>();
        }

        // Метод, отвечающий за инициализацию настроек по умолчанию
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Например, вы можете задать начальные настройки здесь, если это нужно
        }
    }
}
