using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.Services
{
    public class RouteVisualization
    {
        private readonly TubeVisual3D _tubeVisual;

        public RouteVisualization(double diameter, int thetaDiv, Color color, double opacity)
        {
            _tubeVisual = new TubeVisual3D
            {
                Diameter = diameter,
                ThetaDiv = thetaDiv,
                Fill = new SolidColorBrush(color) { Opacity = opacity }
            };
        }

        public ModelVisual3D Model => _tubeVisual;

        public void Build(ObservableCollection<Point3D> points)
        {
            _tubeVisual.Path.Clear();
            foreach (var point in points)
            {
                _tubeVisual.Path.Add(point);
            }
        }
    }
}