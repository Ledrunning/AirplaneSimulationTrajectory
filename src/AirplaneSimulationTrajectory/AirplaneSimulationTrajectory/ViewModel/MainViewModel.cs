using System.Windows;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.View;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class MainViewModel
    {
        public static readonly DependencyProperty CloudsProperty = DependencyProperty.Register(
            "Clouds", typeof(Material), typeof(MainWindow), new UIPropertyMetadata(null));

        /// <summary>
        /// The sunlight direction property.
        /// </summary>
        public static readonly DependencyProperty SunlightDirectionProperty =
            DependencyProperty.Register(
                "SunlightDirection", typeof(Vector3D), typeof(MainWindow), new UIPropertyMetadata(new Vector3D()));
    }
}