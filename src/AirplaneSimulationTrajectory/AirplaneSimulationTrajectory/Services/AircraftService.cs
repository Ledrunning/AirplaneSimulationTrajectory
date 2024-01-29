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
            _pointA = Normalize(from) * EarthRadius;
            _pointB = Normalize(to) * EarthRadius;
            AircraftPosition = _pointA;
        }

        public (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdatePosition()
        {
            // The airplane has arrived at point B
            if ((_pointB - AircraftPosition).Length < MinDistance)
            {
                return (default, _pointB, true);
            }

            // Check if there are points in the tube trajectory
            var tubePathPoints = TrajectoryData.Points?.ToList();
            if (tubePathPoints == null || tubePathPoints.Count < 2)
            {
                return (default, AircraftPosition, false);
            }

            // Find the tube segment containing the current aircraft position
            var segmentIndex = 0;
            Vector3D currentTubePoint;
            Vector3D nextTubePoint = default;
            for (var i = 0; i < tubePathPoints.Count - 1; i++)
            {
                currentTubePoint = (Vector3D)tubePathPoints[i].Point3D;
                nextTubePoint = (Vector3D)tubePathPoints[i + 1].Point3D;

                // Check if the aircraft is within the proximity of the current tube segment
                if ((AircraftPosition - currentTubePoint).Length < MinDistance)
                {
                    segmentIndex = i;
                    break;
                }
            }

            // Move to the next tube segment if the aircraft is close to the end of the current segment
            if ((_pointB - nextTubePoint).Length < MinDistance)
            {
                segmentIndex++;
                if (segmentIndex >= tubePathPoints.Count - 1)
                {
                    // The airplane has arrived at point B
                    return (default, _pointB, true);
                }
            }

            // Move the aircraft along the current tube segment
            currentTubePoint = (Vector3D)tubePathPoints[segmentIndex].Point3D;

            var direction = Normalize(nextTubePoint - currentTubePoint);
            var distanceToNextPoint = (AircraftPosition - currentTubePoint).Length;

            var nextPosition = currentTubePoint + direction * DeltaTime;

            // If the aircraft is getting too far from the current segment, snap it to the next point
            if (distanceToNextPoint > MinDistance)
            {
                nextPosition = nextTubePoint;
            }

            // Apply transform
            var planeTransform = GetPlaneTransform(nextPosition, direction);

            // Default return if no conditions are met
            return (planeTransform, nextPosition, false);
        }

        public Vector3D MovementCalculation(DateTime now, DateTime juneSolstice)
        {
            var declination = 23.45 * Math.Cos((now.DayOfYear - juneSolstice.DayOfYear) / 365.25 * 2 * Math.PI);
            var phi = -now.Hour / 24.0 * Math.PI * 2;
            var theta = declination / 180 * Math.PI;
            return -new Vector3D(Math.Cos(phi) * Math.Cos(theta), Math.Sin(phi) * Math.Cos(theta), Math.Sin(theta));
        }

        public Point3DCollection AddTubeRoutePoints()
        {
            var points = new Point3DCollection();
            foreach (var point in TrajectoryData.Points)
            {
                points.Add(Normalize(point.Point3D));
            }

            return points;
        }

        private Transform3D GetPlaneTransform(Vector3D position, Vector3D direction)
        {
            // Get the up vector by assuming the plane is initially level
            var up = new Vector3D(0, 1, 0);

            // Calculate the rotation axis
            var axis = Vector3D.CrossProduct(up, direction);

            // Calculate the rotation angle
            var angle = Vector3D.AngleBetween(up, direction);

            // Create the rotation transform
            var rotation = new AxisAngleRotation3D(axis, -angle);
            var rotationTransform = new RotateTransform3D(rotation, new Point3D(position.X, position.Y, position.Z));

            return rotationTransform;
        }

        public Point3D Normalize(Point3D point)
        {
            var length = Math.Sqrt(point.X * point.X + point.Y * point.Y + point.Z * point.Z);
            return new Point3D(EarthRadius * point.X / length, EarthRadius * point.Y / length,
                EarthRadius * point.Z / length);
        }

        private static Vector3D Normalize(Vector3D vector)
        {
            return vector / vector.Length;
        }
    }
}