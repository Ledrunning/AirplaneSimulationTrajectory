using System;
using System.Diagnostics;
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

                // Register MainViewModel
                _container.Register<MainViewModel>(Lifestyle.Singleton);

                // Register MainWindow
                _container.Register<MainWindow>(Lifestyle.Singleton);

                // Verify the container's configuration
                _container.Verify();

                // Resolve MainWindow and MainViewModel from the container
                var mainWindow = _container.GetInstance<MainWindow>();
                var mainViewModel = _container.GetInstance<MainViewModel>();

                // Initialize the MainWindow with the MainViewModel
                mainWindow.Initialize(mainViewModel);

                // Show MainWindow
                mainWindow.Show();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Dispose();
            base.OnExit(e);
        }
    }
}