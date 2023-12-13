using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.Services;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private FileModelVisual3D _aircraft;

        private HelixViewport3D _cameraView3D;

        private Material _clouds;

        private HelixViewport3D _mainViewport3D;

        private Vector3D _sunlightDirection;
        private readonly AircraftService _aircraftService;
        private Timer _timer;

        public MainViewModel()
        {
            //TODO USE the DI.
            _aircraftService = new AircraftService();
            
            Clouds = MaterialHelper.CreateImageMaterial("pack://application:,,,/Images/clouds.jpg", 0.5);

            // Initialize the HelixViewport3D instances
            CameraView3D = new HelixViewport3D();
            MainViewport3D = new HelixViewport3D();
            Aircraft = new FileModelVisual3D();

            var now = DateTime.UtcNow;
            var juneSolstice = new DateTime(now.Year, 6, 22);

            Calculation(now, juneSolstice);

            SetAircraftPath();

            InitializeTimer();
        }

        public HelixViewport3D CameraView3D
        {
            get => _cameraView3D;
            set => SetField(ref _cameraView3D, value, nameof(CameraView3D));
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
            _timer = new Timer(30);
            _timer.AutoReset = true;
            _timer.Elapsed += (o, e) => { Task.Run(() => OnTimerTick(null, null)); };
            _timer.Start();
        }

        private void Calculation(DateTime now, DateTime juneSolstice)
        {
            SunlightDirection = _aircraftService.MovementCalculation(now, juneSolstice);
            CameraView3D.Camera.LookDirection = SunlightDirection;
            CameraView3D.Camera.Position = new Point3D() - CameraView3D.Camera.LookDirection;
            CameraView3D.Title = now.ToString("u");
            CameraView3D.TextBrush = Brushes.White;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            var result = _aircraftService.UpdateAircraftPosition();
            Application.Current.Dispatcher.Invoke(() =>
            {
                Aircraft.Transform = result.planeTransform;
                MainViewport3D.InvalidateVisual();
            });

            //new Plane position
            _aircraftService.AircraftPosition = result.secondPosition;
        }
    }
}