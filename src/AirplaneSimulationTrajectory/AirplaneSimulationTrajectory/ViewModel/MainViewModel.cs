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
        private readonly AircraftService aircraftService;
        private Timer timer;

        public MainViewModel()
        {
            //TODO USE the DI.
            aircraftService = new AircraftService();
            Aircraft = new FileModelVisual3D();
            CameraView3D = new HelixViewport3D();
            MainViewport3D = new HelixViewport3D();
            Clouds = MaterialHelper.CreateImageMaterial("pack://application:,,,/Images/clouds.jpg", 0.5);
            
            var now = DateTime.UtcNow;
            var juneSolstice = new DateTime(now.Year, 6, 22);

            Calculation(now, juneSolstice);

            SetAircraftPath();

            InitializeTimer();
        }

        public HelixViewport3D CameraView3D
        {
            get => _cameraView3D;
            set
            {
                _cameraView3D = value;
                SetField(ref _cameraView3D, value, nameof(CameraView3D));
            }
        }

        public HelixViewport3D MainViewport3D
        {
            get => _mainViewport3D;
            set
            {
                _mainViewport3D = value;
                SetField(ref _mainViewport3D, value, nameof(MainViewport3D));
            }
        }

        public FileModelVisual3D Aircraft
        {
            get => _aircraft;
            set
            {
                _aircraft = value;
                SetField(ref _aircraft, value, nameof(Aircraft));
            }
        }

        public Material Clouds
        {
            get => _clouds;
            set
            {
                _clouds = value;
                SetField(ref _clouds, value, nameof(Clouds));
            }
        }

        public Vector3D SunlightDirection
        {
            get => _sunlightDirection;

            set
            {
                _sunlightDirection = value;
                SetField(ref _sunlightDirection, value, nameof(SunlightDirection));
            }
        }

        private void SetAircraftPath()
        {
            //set start and finish pos of plane
            var from = new Vector3D(1, 0, 0);
            var to = new Vector3D(0, 1, 1);
            aircraftService.SetPlanePath(from, to);
        }

        private void InitializeTimer()
        {
            //create timer for updating
            timer = new Timer(30);
            timer.AutoReset = true;
            timer.Elapsed += (o, e) => { Task.Run(() => OnTimerTick(null, null)); };
            timer.Start();
        }

        private void Calculation(DateTime now, DateTime juneSolstice)
        {
            SunlightDirection = aircraftService.MovementCalculation(now, juneSolstice);
            CameraView3D.Camera.LookDirection = SunlightDirection;
            CameraView3D.Camera.Position = new Point3D() - CameraView3D.Camera.LookDirection;
            CameraView3D.Title = now.ToString("u");
            CameraView3D.TextBrush = Brushes.White;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            var result = aircraftService.UpdateAircraftPosition();
            Application.Current.Dispatcher.Invoke(() =>
            {
                Aircraft.Transform = result.planeTransform;
                MainViewport3D.InvalidateVisual();
            });

            //new Plane position
            aircraftService.AircraftPosition = result.secondPosition;
        }
    }
}