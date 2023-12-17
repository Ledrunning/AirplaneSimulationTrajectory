using System;
using System.Windows;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Services;
using AirplaneSimulationTrajectory.View;
using AirplaneSimulationTrajectory.ViewModel;
using HelixToolkit.Wpf;
using SimpleInjector;

namespace AirplaneSimulationTrajectory
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Container _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                _container = new Container();

                // Register services and types
                _container.Register<IAircraftService, AircraftService>(Lifestyle.Singleton);
                _container.Register<HelixViewport3D>(Lifestyle.Singleton);
                _container.Register<FileModelVisual3D>(Lifestyle.Singleton);

                // Register MainViewModel using a factory delegate
                _container.Register<MainViewModel>(() =>
                        new MainViewModel(
                            _container.GetInstance<IAircraftService>(),
                            _container.GetInstance<HelixViewport3D>(),
                            _container.GetInstance<FileModelVisual3D>()
                        ),
                    Lifestyle.Singleton);

                // Register MainWindow using a factory delegate
                _container.Register<MainWindow>(() =>
                    new MainWindow(_container.GetInstance<MainViewModel>()), Lifestyle.Singleton);

                // Verify the container's configuration
                _container.Verify();

                // Resolve MainWindow from the container
                var mainWindow = _container.GetInstance<MainWindow>();

                // Show the main window
                mainWindow.Show();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Dispose();
            base.OnExit(e);
        }
    }
}