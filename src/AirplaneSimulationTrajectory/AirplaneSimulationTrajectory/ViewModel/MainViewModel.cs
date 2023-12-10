using System;
using System.Timers;
using System.Windows;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.View;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Timer timer;

        public MainViewModel()
        {
            Clouds = MaterialHelper.CreateImageMaterial("pack://application:,,,/Images/clouds.jpg", 0.5);
            var now = DateTime.UtcNow;
            var juneSolstice = new DateTime(now.Year, 6, 22);
        }

        private Material _clouds;

        private Vector3D _sunlightDirection;

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
    }
}