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

        public double Diameter
        {
            get => _tubeVisual.Diameter;
            set => _tubeVisual.Diameter = value;
        }

        public int ThetaDiv
        {
            get => _tubeVisual.ThetaDiv;
            set => _tubeVisual.ThetaDiv = value;
        }

        public Brush Fill
        {
            get => _tubeVisual.Fill;
            set => _tubeVisual.Fill = value;
        }

        public ModelVisual3D Model => _tubeVisual;

        public void Build(Point3DCollection points)
        {
            _tubeVisual.Path.Clear();
            foreach (var point in points)
            {
                _tubeVisual.Path.Add(point);
            }
        }
    }
}