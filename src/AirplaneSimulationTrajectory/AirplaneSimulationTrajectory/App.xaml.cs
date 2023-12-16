using AirplaneSimulationTrajectory.View;
using AirplaneSimulationTrajectory.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Services;
using SimpleInjector;

namespace AirplaneSimulationTrajectory
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Container _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _container = new Container();

            _container.Register<IAircraftService, AircraftService>(Lifestyle.Singleton);
            _container.Register<MainViewModel>(Lifestyle.Singleton);

            // Verify the container's configuration
            _container.Verify();

            var mainViewModelInstance = _container.GetInstance<MainViewModel>();
            // Create the main window and set its DataContext to the MainViewModel
            try
            {
                Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow
                    {
                        DataContext = mainViewModelInstance
                    };
                    mainWindow.Show();
                });

            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            //mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Dispose();
            base.OnExit(e);
        }
    }
}
