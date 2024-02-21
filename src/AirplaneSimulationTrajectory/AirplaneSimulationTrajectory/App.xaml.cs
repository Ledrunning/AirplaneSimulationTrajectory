using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Services;
using AirplaneSimulationTrajectory.View;
using AirplaneSimulationTrajectory.ViewModel;
using CommonConfiguration;
using CommonConfiguration.Configuration.Model;
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

                _container.Register<Settings>(Lifestyle.Singleton);

                var mainConfiguration = new MainConfiguration();
                var settings = mainConfiguration.GetSettings();

                _container.Register<MainConfiguration>(Lifestyle.Singleton);

                // Register services and types
                var colorFromString = (string.IsNullOrWhiteSpace(settings.TubeConfiguration.Color)
                    ? (Color)ColorConverter.ConvertFromString(settings.TubeConfiguration.Color)
                    : Colors.Red);

                _container.Register(() =>
                    new RouteVisualization(settings.TubeConfiguration.Diameter, 10, colorFromString, 1.0), Lifestyle.Singleton);

                _container.Register<IAircraftService, AircraftService>(Lifestyle.Singleton);
                _container.Register<IFlightInfoViewModel, FlightInfoViewModel>(Lifestyle.Singleton);
                _container.Register<HelixViewport3D>(Lifestyle.Singleton);
                _container.Register<FileModelVisual3D>(Lifestyle.Singleton);
                _container.Register<MainViewModel>(Lifestyle.Singleton);
                _container.Register<MainWindow>(Lifestyle.Singleton);

                // RegisterInitializer to set MainViewModel in MainWindow
                _container.RegisterInitializer<MainWindow>(mainWindow =>
                {
                    var mainViewModel = _container.GetInstance<MainViewModel>();
                    mainWindow.Initialize(mainViewModel);
                    mainWindow.Show();
                });

                // Verify the container's configuration
                _container.Verify();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
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