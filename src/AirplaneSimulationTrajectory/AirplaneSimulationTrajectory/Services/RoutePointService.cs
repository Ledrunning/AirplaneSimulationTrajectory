using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using AirplaneSimulationTrajectory.Model;
using HelixToolkit.Wpf;

namespace AirplaneSimulationTrajectory.Services
{
    public class RoutePointService : TubeVisual3D
    {
        public RoutePointService(double diameter, int thetaDiv, Color color, double opacity)
        {
            Diameter = diameter;
            ThetaDiv = thetaDiv;
            Material = MaterialHelper.CreateMaterial(color, opacity);
        }

        public void AddPoint(RoutePointModel point)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => Path.Add(point.Point3D)));
        }

        public void Build(List<RoutePointModel> points)
        {
            Path.Clear();
            foreach (var point in points)
            {
                AddPoint(point);
            }
        }
    }
}