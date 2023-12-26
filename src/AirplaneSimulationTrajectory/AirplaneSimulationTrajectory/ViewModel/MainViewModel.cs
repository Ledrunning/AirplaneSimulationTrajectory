using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Services;
using AirplaneSimulationTrajectory.ViewModel.Command;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IAircraftService _aircraftService;
        private FileModelVisual3D _aircraft;
        private Material _clouds;
        private HelixViewport3D _mainViewport3D;
        private Vector3D _sunlightDirection;
        private Timer _timer;

        public MainViewModel(IAircraftService aircraftService, 
            HelixViewport3D helixViewport3D, 
            FileModelVisual3D fileModelVisual3D)
        {
            _aircraftService = aircraftService;
            Clouds = MaterialHelper.CreateImageMaterial("pack://application:,,,/Images/clouds.jpg", 0.5);

            // Initialize the HelixViewport3D instances
            MainViewport3D = helixViewport3D;
            Aircraft = fileModelVisual3D;
            InitializeCommand();
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

        private void SetAircraftPath()
        {
            //set start and finish pos of plane
            var from = new Vector3D(1, 0, 0);
            var to = new Vector3D(0, 1, 1);
            _aircraftService.SetPlanePath(from, to);
        }

        private void InitializeTimer()
        {
            //create timer for updating
            _timer = new Timer(30)
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

        private void Calculation(DateTime now, DateTime juneSolstice)
        {
            SunlightDirection = _aircraftService.MovementCalculation(now, juneSolstice);
            MainViewport3D.Camera.LookDirection = SunlightDirection;
            MainViewport3D.Camera.Position = new Point3D() - MainViewport3D.Camera.LookDirection;
            MainViewport3D.Title = now.ToString("u");
            MainViewport3D.TextBrush = Brushes.White;
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
                    return;
                }

                Aircraft.Transform = planeTransform;
                MainViewport3D.InvalidateVisual();

                // Set the new position of the airplane
                _aircraftService.AircraftPosition = secondPosition;
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

        public ICommand FlightStart { get; private set; }

        private void InitializeCommand()
        {
            FlightStart = new RelayCommand(StartCommand);
        }

        public void StartCommand()
        {
            var now = DateTime.UtcNow;
            var juneSolstice = new DateTime(now.Year, 6, 22);
            Calculation(now, juneSolstice);
            SetAircraftPath();
            InitializeTimer();
        }
    }
}