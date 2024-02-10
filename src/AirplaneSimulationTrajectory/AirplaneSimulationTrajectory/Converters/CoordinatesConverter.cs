using System;
using System.Windows.Media.Media3D;

namespace AirplaneSimulationTrajectory.Converters
{
    public class CoordinatesConverter
    {
        public static Point3D CoordinatesToPoint3D(double latitude, double longitude, double radius)
        {
            longitude -= 180;
            latitude = latitude / 180 * Math.PI;
            longitude = longitude / 180 * Math.PI;
            return new Point3D(radius * Math.Cos(latitude) * Math.Cos(longitude),
                radius * Math.Cos(latitude) * Math.Sin(longitude), radius * Math.Sin(latitude));
        }

        public static Point3D Vector3DToPoint3D(Vector3D vector3D)
        {
            return new Point3D(vector3D.X, vector3D.Y, vector3D.Z);
        }

        public static void Point3DToCoordinates(Point3D pt, out double lat, out double lon)
        {
            lon = Math.Atan2(pt.Y, pt.X) * 180 / Math.PI;
            lon += 180;
            if (lon > 180)
            {
                lon -= 360;
            }

            if (lon < -180)
            {
                lon += 360;
            }

            var a = Math.Sqrt(pt.X * pt.X + pt.Y * pt.Y);
            lat = Math.Atan2(pt.Z, a) * 180 / Math.PI;
        }

        public static Vector3D Point3DToVector3D(Point3D point3D)
        {
            return new Vector3D(point3D.X, point3D.Y, point3D.Z);
        }

        public static double GetDistance(Vector3D point1, Vector3D point2)
        {
            return (point1 - point2).Length;
        }

        public static void Vector3DToCoordinates(Vector3D vector, out double latitude, out double longitude)
        {
            // Spherical coordinates
            var radius = vector.Length;
            var inclination = Math.Acos(vector.Z / radius);
            var azimuth = Math.Atan2(vector.Y, vector.X);

            // Convert radians to grad
            latitude = inclination * (180 / Math.PI);
            longitude = azimuth * (180 / Math.PI);
        }
    }
}