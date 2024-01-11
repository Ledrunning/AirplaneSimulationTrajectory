using System;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.Contracts;

namespace AirplaneSimulationTrajectory.Services
{
    internal class AircraftService : IAircraftService
    {
        private const double EarthRadius = 1;
        private const double PlaneScale = 1;
        private const double DeltaTime = 0.001;
        private const double HeightOverGround = 0.02;
        private const double MinDistance = 0.01f; // Current airplane position
        private Vector3D _pointA; // From point
        private Vector3D _pointB; // To point
        public Vector3D AircraftPosition { get; set; }

        public (Vector3D pointA, Vector3D pointB) SetPlanePath(Vector3D from, Vector3D to)
        {
            _pointA = Normalized(from) * EarthRadius;
            _pointB = Normalized(to) * EarthRadius;
            AircraftPosition = _pointA;
            return (_pointA, _pointB);
        }

        public (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdateAircraftPosition()
        {
            // The airplane has arrived at point B
            if ((_pointB - AircraftPosition).Length < MinDistance)
            {
                return (default, _pointB, true);
            }

            // Calculate the orientation and new position of the airplane
            var firstPosition = AircraftPosition + Normalized(_pointB - AircraftPosition) * DeltaTime;
            var secondPosition = EarthRadius * Normalized(firstPosition);
            var aircraftDirections = secondPosition - AircraftPosition;

            // The forward and upward direction of the airplane
            var forward = Normalized(aircraftDirections);
            var up = Normalized(firstPosition);

            // Apply transform
            var planeTransform = GetPlaneTransform(forward, up, firstPosition * (1 + HeightOverGround));

            return (planeTransform, secondPosition, false);
        }

        public Vector3D MovementCalculation(DateTime now, DateTime juneSolstice)
        {
            var declination = 23.45 * Math.Cos((now.DayOfYear - juneSolstice.DayOfYear) / 365.25 * 2 * Math.PI);
            var phi = -now.Hour / 24.0 * Math.PI * 2;
            var theta = declination / 180 * Math.PI;
            return -new Vector3D(Math.Cos(phi) * Math.Cos(theta), Math.Sin(phi) * Math.Cos(theta), Math.Sin(theta));
        }

        private static Vector3D Normalized(Vector3D vector)
        {
            return vector / vector.Length;
        }

        public Point3D NormalizePoint(Point3D point)
        {
            var length = Math.Sqrt(point.X * point.X + point.Y * point.Y + point.Z * point.Z);
            return new Point3D(EarthRadius * point.X / length, EarthRadius * point.Y / length, EarthRadius * point.Z / length);
        }

        private Transform3D GetPlaneTransform(Vector3D forward, Vector3D up, Vector3D position)
        {
            var v1 = forward;
            var v3 = up;
            var v2 = Vector3D.CrossProduct(v1, v3);

            var matrix3D = new Matrix3D(
                v1.X, v1.Y, v1.Z, 0,
                v2.X, v2.Y, v2.Z, 0,
                v3.X, v3.Y, v3.Z, 0,
                0, 0, 0, 1);

            matrix3D.Scale(new Vector3D(PlaneScale, PlaneScale, PlaneScale));

            matrix3D.Translate(position);

            return new MatrixTransform3D(matrix3D);
        }
    }
}