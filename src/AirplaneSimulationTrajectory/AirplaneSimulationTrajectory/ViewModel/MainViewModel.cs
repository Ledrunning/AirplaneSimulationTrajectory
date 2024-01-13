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

            // Initialize the HelixViewport3D instances
            MainViewport3D = helixViewport3D;
            Aircraft = fileModelVisual3D;
            InitializeCommand();
            InitializeAircraftPosition();

            // Create and initialize RouteVisualization
            RouteVisualization = routeVisualization;
            MainViewport3D.Children.Add(_routeVisualization.Model);

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
            // set start and finish pos of plane
            var f = TrajectoryData.Points[0].Point3D;
            var l = TrajectoryData.Points[17].Point3D;
            var from = new Vector3D(1, 0, 0);
            var to = new Vector3D(0, 1, 1);
            _aircraftService.SetPlanePath(new Vector3D(f.X, f.Y, f.Z), new Vector3D(l.X, l.Y, l.Z));
        }

        private void InitializeAircraftPosition()
        {
            var now = DateTime.UtcNow;
            var juneSolstice = new DateTime(now.Year, 6, 22);
            Calculation(now, juneSolstice);
            SetAircraftPath();
        }

        private void Calculation(DateTime now, DateTime juneSolstice)
        {
            SunlightDirection = _aircraftService.MovementCalculation(now, juneSolstice);

            // Assuming the tube is along the Z-axis, adjust the camera position accordingly
            var cameraPosition = SunlightDirection * 10; // Adjust the multiplier as needed
            var targetPosition = new Point3D(); // Adjust the target position if needed

            // Set the camera's position and look direction
            MainViewport3D.Camera.Position = new Point3D(cameraPosition.X, cameraPosition.Y, cameraPosition.Z);
            MainViewport3D.Camera.LookDirection = targetPosition - MainViewport3D.Camera.Position;

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