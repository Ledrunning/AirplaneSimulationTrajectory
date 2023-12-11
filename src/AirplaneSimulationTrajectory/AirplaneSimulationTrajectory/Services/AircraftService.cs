using System;
using System.Windows.Media.Media3D;

namespace AirplaneSimulationTrajectory.Services
{
    internal class AircraftService
    {
        private readonly double EarthRadius = 1;
        private readonly double PlaneScale = 1;
        private readonly double deltaTime = 0.001;
        private readonly double HeightOverGround = 0.02;
        private readonly double MinDistance = 0.01f; //current plane pos
        private Vector3D PointA; //from point
        private Vector3D PointB; //to point
        public Vector3D AircraftPosition { get; set; } //current plane pos

        public void SetPlanePath(Vector3D from, Vector3D to)
        {
            PointA = Normalized(from) * EarthRadius;
            PointB = Normalized(to) * EarthRadius;
            AircraftPosition = PointA;
        }

        private static Vector3D Normalized(Vector3D vector)
        {
            return vector / vector.Length;
        }

        public (Transform3D planeTransform, Vector3D secondPosition) UpdateAircraftPosition()
        {
            //aircraft are arrived in point B
            if ((PointB - AircraftPosition).Length < MinDistance)
            {
                return default;
            }

            //calculate orientation and new position of an aircraft
            var firstPosition = AircraftPosition + Normalized(PointB - AircraftPosition) * deltaTime;
            var secondPosition = EarthRadius * Normalized(firstPosition);
            var aircraftDirections = secondPosition - AircraftPosition;

            //plane forward and up direction
            var forward = Normalized(aircraftDirections);
            var up = Normalized(firstPosition);

            //apply transform
            var planeTransform = GetPlaneTransform(forward, up, firstPosition * (1 + HeightOverGround));

            return (planeTransform, secondPosition);
        }

        public Transform3D GetPlaneTransform(Vector3D forward, Vector3D up, Vector3D pos)
        {
            //look at
            var v1 = forward;
            var v3 = up;
            var v2 = Vector3D.CrossProduct(v1, v3);

            var matrix3D = new Matrix3D(
                v1.X, v1.Y, v1.Z, 0,
                v2.X, v2.Y, v2.Z, 0,
                v3.X, v3.Y, v3.Z, 0,
                0, 0, 0, 1);

            //scale
            matrix3D.Scale(new Vector3D(PlaneScale, PlaneScale, PlaneScale));

            //pos
            matrix3D.Translate(pos);

            return new MatrixTransform3D(matrix3D);
        }

        public Vector3D CalculateMove(DateTime now, DateTime juneSolstice)
        {
            // todo: check calculation - this is probably not correct
            var declination = 23.45 * Math.Cos((now.DayOfYear - juneSolstice.DayOfYear) / 365.25 * 2 * Math.PI);
            var phi = -now.Hour / 24.0 * Math.PI * 2;
            var theta = declination / 180 * Math.PI;
            return -new Vector3D(Math.Cos(phi) * Math.Cos(theta), Math.Sin(phi) * Math.Cos(theta), Math.Sin(theta));
        }
    }
}