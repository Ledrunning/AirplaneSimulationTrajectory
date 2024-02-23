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
            
            lat = Math.Atan2(pt.Z, Math.Sqrt(pt.X * pt.X + pt.Y * pt.Y)) * 180 / Math.PI;
        }
    }
}