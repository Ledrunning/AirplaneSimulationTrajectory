using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using AirplaneSimulationTrajectory.Model;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.Services
{
    public class RouteVisualization : DependencyObject
    {
        public static readonly DependencyProperty TubeVisualProperty =
            DependencyProperty.Register("TubeVisual", typeof(TubeVisual3D), typeof(RouteVisualization));

        public RouteVisualization(double diameter, int thetaDiv, Color color, double opacity)
        {
            TubeVisual = new TubeVisual3D
            {
                Diameter = diameter,
                ThetaDiv = thetaDiv,
                Material = MaterialHelper.CreateMaterial(color, opacity)
            };
        }

        public TubeVisual3D TubeVisual
        {
            get => (TubeVisual3D)GetValue(TubeVisualProperty);
            set => SetValue(TubeVisualProperty, value);
        }

        public void AddPoint(RoutePointModel point)
        {
            if (Application.Current?.Dispatcher != null)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(() => TubeVisual.Path.Add(point.Point3D)));
            }
        }

        public void Build(List<RoutePointModel> points)
        {
            TubeVisual.Path.Clear();
            foreach (var point in points)
            {
                AddPoint(point);
            }
        }
    }
}