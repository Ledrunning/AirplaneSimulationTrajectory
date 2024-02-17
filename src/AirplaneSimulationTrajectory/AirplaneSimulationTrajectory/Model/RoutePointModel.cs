using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.Constants;
using AirplaneSimulationTrajectory.Converters;

namespace AirplaneSimulationTrajectory.Model
{
    public class RoutePointModel
    {
        public double Latitude;

        public double Longitude;
        public Point3D Point3D;

        public RoutePointModel(double latitude, double longitude, double? radius = null)
        {
            if (radius == null)
            {
                radius = AppConstants.EarthRadius;
            }

            Longitude = longitude;
            Latitude = latitude;
            Point3D = CoordinatesConverter.CoordinatesToPoint3D(latitude, longitude, (double)radius);
        }

        // Convert data to spherical
        public RoutePointModel(Point3D point)
        {
            Point3D = point;
            CoordinatesConverter.Point3DToCoordinates(Point3D, out Latitude, out Longitude);
        }
    }
}