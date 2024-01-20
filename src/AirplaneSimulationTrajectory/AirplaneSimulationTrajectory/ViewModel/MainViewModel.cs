using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Converters;
using AirplaneSimulationTrajectory.Model;
using AirplaneSimulationTrajectory.Services;
using AirplaneSimulationTrajectory.ViewModel.Command;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAircraftService _aircraftService;
        private RouteVisualization _routeVisualization;
        private FileModelVisual3D _aircraft;
        private Material _clouds;

        private double _latitude;
        private double _longitude;
        private HelixViewport3D _mainViewport3D;
        private Vector3D _sunlightDirection;
        private Timer _timer;

        private Point3DCollection _tubePathPoints = new Point3DCollection();

        public MainViewModel(
            IAircraftService aircraftService,
            IFlightInfoViewModel flightInfoViewModel,
            HelixViewport3D helixViewport3D,
            FileModelVisual3D fileModelVisual3D,
            RouteVisualization routeVisualization)
        {
            _aircraftService = aircraftService;
            FlightInfoViewModel = flightInfoViewModel;
            //Clouds = MaterialHelper.CreateImageMaterial("pack://application:,,,/Images/clouds.jpg", 0.5);

            MainViewport3D = helixViewport3D;
            Aircraft = fileModelVisual3D;

            RouteVisualization = routeVisualization;
            MainViewport3D.Children.Add(RouteVisualization.Model);

            InitializeCommand();
            InitializeAircraftPosition();

            TubePathPoints = _aircraftService.AddTubeRoutePoints();
            _routeVisualization.Build(TubePathPoints);
        }

        public RouteVisualization RouteVisualization
        {
            get => _routeVisualization;
            set => SetField(ref _routeVisualization, value);
        }

        public HelixViewport3D MainViewport3D
        {
            get => _mainViewport3D;
            set => SetField(ref _mainViewport3D, value);
        }

        public FileModelVisual3D Aircraft
        {
            get => _aircraft;
            set => SetField(ref _aircraft, value, nameof(Aircraft));
        }

        public Material Clouds
        {
            get => _clouds;
            set => SetField(ref _clouds, value, nameof(Clouds));
        }

        public Vector3D SunlightDirection
        {
            get => _sunlightDirection;
            set => SetField(ref _sunlightDirection, value, nameof(SunlightDirection));
        }

        public Point3DCollection TubePathPoints
        {
            get => _tubePathPoints;
            set => SetField(ref _tubePathPoints, value, nameof(TubePathPoints));
        }

        public ICommand FlightStart { get; private set; }

        public IFlightInfoViewModel FlightInfoViewModel { get; }

        private void SetAircraftPath()
        {
            var start = TrajectoryData.Points.First().Point3D;
            //var end = TrajectoryData.Points.Last().Point3D;
            var end = TrajectoryData.Points[16].Point3D;
            _aircraftService.SetPlanePath(new Vector3D(start.X, start.Y, start.Z), new Vector3D(end.X, end.Y, end.Z));
        }

        private void InitializeAircraftPosition()
        {
            Calculation();
            SetAircraftPath();
        }

        private void Calculation()
        {
            var now = DateTime.UtcNow;
            var juneSolstice = new DateTime(now.Year, 6, 22);

            SunlightDirection = _aircraftService.MovementCalculation(now, juneSolstice);

            SunlightDirection = _aircraftService.MovementCalculation(now, juneSolstice);
            MainViewport3D.Camera.LookDirection = SunlightDirection;
            MainViewport3D.Camera.Position = new Point3D() - MainViewport3D.Camera.LookDirection;
            MainViewport3D.Title = now.ToString("u");
            MainViewport3D.TextBrush = Brushes.White;
        }

        private void InitializeTimer()
        {
            // create timer for updating every 100 ms
            _timer = new Timer(1 * 100)
            {
                AutoReset = true,
                Enabled = true
            };
            _timer.Elapsed += (o, e) =>
            {
                if (!Application.Current.Dispatcher.HasShutdownStarted)
                {
                    Application.Current.Dispatcher.Invoke(() => OnTimerTick(null, null));
                }
            };
            _timer.Start();
        }

        private void StopTimer()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Stop();
            _timer.Elapsed -= OnTimerTick;
            _timer.Dispose();
        }

        private void OnUIThreadTimerTick()
        {
            if (_aircraftService == null || Aircraft == null)
            {
                return;
            }

            try
            {
                var (planeTransform, secondPosition, resetTimer) = _aircraftService.UpdateAircraftPosition();

                if (resetTimer)
                {
                    StopTimer();
                    FlightInfoViewModel.ClearFields();
                    return;
                }

                Aircraft.Transform = planeTransform;
                MainViewport3D.InvalidateVisual();

                // Set the new position of the airplane
                _aircraftService.AircraftPosition = secondPosition;

                CoordinatesConverter.Point3DToCoordinates(
                    CoordinatesConverter.Vector3DToPoint3D(secondPosition),
                    out _latitude, out _longitude);
                FlightInfoViewModel.UpdateData(_latitude, _longitude);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(e);
            }
        }
        
        private void OnTimerTick(object sender, EventArgs e)
        {
            OnUIThreadTimerTick();
        }

        private void InitializeCommand()
        {
            FlightStart = new RelayCommand(StartCommand);
        }

        public void StartCommand()
        {
            InitializeAircraftPosition();
            InitializeTimer();
            FlightInfoViewModel.InitializeData();
        }
    }
}