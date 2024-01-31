using System;
using System.Linq;
using System.Windows.Media.Media3D;
using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.Model;

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

        public void SetPlanePath(Vector3D from, Vector3D to)
        {
            _pointA = Normalized(from) * EarthRadius;
            _pointB = Normalized(to) * EarthRadius;
            AircraftPosition = _pointA;
        }

        // Linear route from A to B point
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

        public (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdateInterpolatePosition()
        {
            // The airplane has arrived at point B
            if ((_pointB - AircraftPosition).Length < MinDistance)
            {
                return (default, _pointB, true);
            }

            // Check if there are points in the tube trajectory
            var tubePathPoints = TrajectoryData.Points.ToList();
            if (tubePathPoints.Count < 2)
            {
                return (default, AircraftPosition, false);
            }

            // Find the tube segment containing the current aircraft position
            var segmentIndex = 0;
            Vector3D currentTubePoint;
            Vector3D nextTubePoint;
            for (var i = 0; i < tubePathPoints.Count - 1; i++)
            {
                currentTubePoint = (Vector3D)tubePathPoints[i].Point3D;
                nextTubePoint = (Vector3D)tubePathPoints[i + 1].Point3D;

                // Check if the aircraft is within the proximity of the current tube segment
                if (!((AircraftPosition - currentTubePoint).Length < MinDistance))
                {
                    continue;
                }

                segmentIndex = i;
                break;
            }

            // Move to the next tube segment if the aircraft is close to the end of the current segment
            if ((AircraftPosition - (Vector3D)tubePathPoints[segmentIndex + 1].Point3D).Length < MinDistance)
            {
                segmentIndex++;
                if (segmentIndex >= tubePathPoints.Count - 1)
                {
                    // The airplane has arrived at point B
                    return (default, _pointB, true);
                }
            }

            // Interpolate the position along the current tube segment
            currentTubePoint = (Vector3D)tubePathPoints[segmentIndex].Point3D;
            nextTubePoint = (Vector3D)tubePathPoints[segmentIndex + 1].Point3D;

            var alpha = (_pointB - currentTubePoint).Length / (nextTubePoint - currentTubePoint).Length;
            var interpolatedPosition = (1 - alpha) * currentTubePoint + alpha * nextTubePoint;

            // Calculate the orientation and new position of the airplane
            var directionOnTube = Normalized(nextTubePoint - currentTubePoint);
            var forward = directionOnTube;
            var up = new Vector3D(0, 1, 0);

            // Apply transform
            var planeTransform = GetPlaneTransform(forward, up, interpolatedPosition * (1 + HeightOverGround));

            return (planeTransform, interpolatedPosition, false);
        }


        public Vector3D MovementCalculation(DateTime now, DateTime juneSolstice)
        {
            var declination = 23.45 * Math.Cos((now.DayOfYear - juneSolstice.DayOfYear) / 365.25 * 2 * Math.PI);
            var phi = -now.Hour / 24.0 * Math.PI * 2;
            var theta = declination / 180 * Math.PI;
            return -new Vector3D(Math.Cos(phi) * Math.Cos(theta), Math.Sin(phi) * Math.Cos(theta), Math.Sin(theta));
        }

        public Point3D NormalizePoint(Point3D point)
        {
            var length = Math.Sqrt(point.X * point.X + point.Y * point.Y + point.Z * point.Z);
            return new Point3D(EarthRadius * point.X / length, EarthRadius * point.Y / length,
                EarthRadius * point.Z / length);
        }

        public Point3DCollection AddTubeRoutePoints()
        {
            var points = new Point3DCollection();
            foreach (var point in TrajectoryData.Points)
            {
                points.Add(NormalizePoint(point.Point3D));
            }

            return points;
        }

        //+
        private static Vector3D Normalized(Vector3D vector)
        {
            return vector / vector.Length;
        }

        //+
        private static Transform3D GetPlaneTransform(Vector3D forward, Vector3D up, Vector3D position)
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