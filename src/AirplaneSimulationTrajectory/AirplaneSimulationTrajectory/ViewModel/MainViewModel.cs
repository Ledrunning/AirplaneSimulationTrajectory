using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using AirplaneSimulationTrajectory.Constants;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Converters;
using AirplaneSimulationTrajectory.Helpers;
using AirplaneSimulationTrajectory.Model;
using AirplaneSimulationTrajectory.Services;
using AirplaneSimulationTrajectory.ViewModel.Command;
using CommonConfiguration.Configuration.Model;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAircraftService _aircraftService;
        private readonly Settings _settings;
        private readonly CustomLinkedList<RoutePointModel> _flightCoordinates;
        private FileModelVisual3D _aircraft;

        private Material _clouds;
        private int _counter;

        private HelixViewport3D _mainViewport3D;

        private RouteVisualization _routeVisualization;
        private Vector3D _sunlightDirection;
        private DispatcherTimer _timer;
        private Point3DCollection _tubePathPoints = new Point3DCollection();

        public MainViewModel(
            IAircraftService aircraftService,
            IFlightInfoViewModel flightInfoViewModel,
            HelixViewport3D helixViewport3D,
            FileModelVisual3D fileModelVisual3D,
            RouteVisualization routeVisualization,
            Settings settings)
        {
            _flightCoordinates = TrajectoryData.GetRoute();
            _aircraftService = aircraftService;
            _settings = settings;
            FlightInfoViewModel = flightInfoViewModel;
            //Clouds = MaterialHelper.CreateImageMaterial("pack://application:,,,/Images/clouds.jpg", 0.5);

            MainViewport3D = helixViewport3D;
            Aircraft = fileModelVisual3D;

            RouteVisualization = routeVisualization;
            MainViewport3D.Children.Add(RouteVisualization.Model);

            InitializeCommand();
            InitializeAircraftPosition();
        }

        public RouteVisualization RouteVisualization
        {
            get => _routeVisualization;
            set => SetField(ref _routeVisualization, value, nameof(RouteVisualization));
        }

        public HelixViewport3D MainViewport3D
        {
            get => _mainViewport3D;
            set => SetField(ref _mainViewport3D, value, nameof(MainViewport3D));
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
            var start = _flightCoordinates.First().Point3D;
            var end = _flightCoordinates.Last().Point3D;
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
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += OnTimerTick;
            _timer.Start();
        }

        private void StopTimer()
        {
            if (_timer == null)
            {
                return;
            }

            _timer.Stop();
            _timer.Tick -= OnTimerTick;
            _timer = null;
        }

        private void OnUIThreadTimerTick()
        {
            if (_aircraftService == null || Aircraft == null)
            {
                return;
            }

            try
            {
                var (planeTransform, secondPosition, resetTimer) = _aircraftService.UpdatePosition();

                if (resetTimer)
                {
                    StopTimer();

                    if (TubePathPoints.Any())
                    {
                        TubePathPoints.Clear();
                    }

                    FlightInfoViewModel.ClearFields();
                    return;
                }

                Aircraft.Transform = planeTransform;

                // Set the new position of the airplane
                _aircraftService.AircraftPosition = secondPosition;

                CoordinatesConverter.Point3DToCoordinates(
                    CoordinatesConverter.Vector3DToPoint3D(secondPosition),
                    out var latitude, out var longitude);
                FlightInfoViewModel.UpdateData(latitude, longitude);

                _counter++;

                if (_counter == AppConstants.PointBuildDelta)
                {
                    AddTubePathPoint(_aircraftService.NormalizePoint(new RoutePointModel(latitude, longitude,
                        AppConstants.RadiusDelta + AppConstants.EarthRadius).Point3D));
                    _routeVisualization.Build(TubePathPoints);
                    _counter = 0;
                }

                MainViewport3D.InvalidateVisual();
            }
            catch (Exception e)
            {
                _counter = 0;
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine(e);
            }
        }

        public void AddTubePathPoint(Point3D point)
        {
            var updatedCollection = new Point3DCollection(_tubePathPoints) { point };
            SetField(ref _tubePathPoints, updatedCollection, nameof(TubePathPoints));
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