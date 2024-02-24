using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Services;
using AirplaneSimulationTrajectory.View;
using AirplaneSimulationTrajectory.ViewModel;
using CommonConfiguration;
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

                // Register settings and main configuration
                var mainConfiguration = new MainConfiguration();
                _container.RegisterInstance(mainConfiguration);

                // Register RouteVisualization instance
                var settings = mainConfiguration.GetSettings();
                var colorFromString = string.IsNullOrWhiteSpace(settings.TubeConfiguration.Color)
                    ? Colors.Red
                    : (Color)ColorConverter.ConvertFromString(settings.TubeConfiguration.Color);
                var routeVisualization =
                    new RouteVisualization(settings.TubeConfiguration.Diameter, settings.TubeConfiguration.ThetaDiv,
                        colorFromString, settings.TubeConfiguration.Opacity);
                _container.RegisterInstance(routeVisualization);

                // Register services and types
                _container.RegisterSingleton<IAircraftService, AircraftService>();
                _container.RegisterSingleton<HelixViewport3D>();
                _container.RegisterSingleton<FileModelVisual3D>();

                _container.Register<IFlightInfoViewModel>(() => new FlightInfoViewModel(settings), Lifestyle.Singleton);

                // Register MainViewModel with dependencies
                _container.RegisterSingleton(() =>
                {
                    var aircraftService = _container.GetInstance<IAircraftService>();
                    var flightInfoViewModel = _container.GetInstance<IFlightInfoViewModel>();
                    var helixViewport3D = _container.GetInstance<HelixViewport3D>();
                    var fileModelVisual3D = _container.GetInstance<FileModelVisual3D>();

                    return new MainViewModel(aircraftService, flightInfoViewModel, helixViewport3D, fileModelVisual3D,
                        routeVisualization, settings);
                });

                // Register MainWindow
                _container.RegisterSingleton<MainWindow>();

                // Initialize and show MainWindow
                var mainWindow = _container.GetInstance<MainWindow>();
                var mainViewModel = _container.GetInstance<MainViewModel>();
                mainWindow.Initialize(mainViewModel);
                mainWindow.Show();

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