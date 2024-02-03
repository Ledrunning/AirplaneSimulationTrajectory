using System;
using System.Diagnostics;
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
        private const double HeightOverGround = 0.01;
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

        private int _currentTubeIndex = 0;
        public (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdateInterpolatePosition()
        {
            // The airplane has arrived at the end of the tube
            if (_currentTubeIndex >= TrajectoryData.Points.Count - 1)
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
            var currentTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex].Point3D;
            var nextTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex + 1].Point3D;

            // Move to the next tube segment if the aircraft is close to the end of the current segment
            if ((AircraftPosition - nextTubePoint).Length < MinDistance)
            {
                _currentTubeIndex++;
                if (_currentTubeIndex >= tubePathPoints.Count - 1)
                {
                    // The airplane has arrived at the end of the tube
                    return (default, _pointB, true);
                }

                // Update current and next tube points
                currentTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex].Point3D;
                nextTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex + 1].Point3D;
            }

            // Interpolate the position along the current tube segment
            var alpha = (AircraftPosition - currentTubePoint).Length / (nextTubePoint - currentTubePoint).Length;
            var interpolatedPosition = (1 - alpha) * currentTubePoint + alpha * nextTubePoint;

            // Calculate the orientation and new position of the airplane
            var firstPosition = AircraftPosition + Normalized(interpolatedPosition - AircraftPosition) * DeltaTime;
            var secondPosition = EarthRadius * Normalized(firstPosition);
            var aircraftDirections = secondPosition - AircraftPosition;

            // The forward and upward direction of the airplane
            var forward = Normalized(aircraftDirections);
            var up = Normalized(firstPosition);

            // Apply transform
            var planeTransform = GetPlaneTransform(forward, up, firstPosition * (1 + HeightOverGround));

            // Default return if no conditions are met
            return (planeTransform, secondPosition, false);
        }

        public (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdatePosition()
        {
            // The airplane has arrived at the end of the tube
            if (_currentTubeIndex >= TrajectoryData.Points.Count - 1)
            {
                Debug.WriteLine("Airplane has arrived at the end of the tube");
                return (default, _pointB, true);
            }

            // Check if there are points in the tube trajectory
            var tubePathPoints = TrajectoryData.Points.ToList();
            if (tubePathPoints.Count < 2)
            {
                Debug.WriteLine("Not enough points in the tube trajectory");
                return (default, AircraftPosition, false);
            }

            // Find the tube segment containing the current aircraft position
            var currentTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex].Point3D;
            var nextTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex + 1].Point3D;

            // Move to the next tube segment if the aircraft is close to the end of the current segment
            if ((AircraftPosition - nextTubePoint).Length < MinDistance)
            {
                _currentTubeIndex++;
                Debug.WriteLine($"Updated _currentTubeIndex: {_currentTubeIndex}");

                if (_currentTubeIndex >= tubePathPoints.Count - 1)
                {
                    Debug.WriteLine("Airplane has arrived at the end of the tube");
                    // The airplane has arrived at the end of the tube
                    return (default, _pointB, true);
                }

                // Update current and next tube points
                currentTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex].Point3D;
                nextTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex + 1].Point3D;

                Debug.WriteLine($"New currentTubePoint: {currentTubePoint}");
                Debug.WriteLine($"New nextTubePoint: {nextTubePoint}");
            }

            // Interpolate the position along the current tube segment
            var alpha = (AircraftPosition - currentTubePoint).Length / (nextTubePoint - currentTubePoint).Length;
            var interpolatedPosition = (1 - alpha) * currentTubePoint + alpha * nextTubePoint;

            // Calculate the orientation and new position of the airplane
            var firstPosition = AircraftPosition + Normalized(interpolatedPosition - AircraftPosition) * DeltaTime;
            var secondPosition = EarthRadius * Normalized(firstPosition);
            var aircraftDirections = secondPosition - AircraftPosition;

            // The forward and upward direction of the airplane
            var forward = Normalized(aircraftDirections);
            var up = Normalized(firstPosition);

            Debug.WriteLine($"AircraftPosition: {AircraftPosition}");
            Debug.WriteLine($"Interpolated Position: {interpolatedPosition}");
            Debug.WriteLine($"First Position: {firstPosition}");
            Debug.WriteLine($"Second Position: {secondPosition}");
            Debug.WriteLine($"Forward: {forward}");
            Debug.WriteLine($"Up: {up}");

            // Apply transform
            var planeTransform = GetPlaneTransform(forward, up, firstPosition * (1 + HeightOverGround));

            // Default return if no conditions are met
            return (planeTransform, secondPosition, false);
        }

        private static double PositionOffset = 10.0; 

        public (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) MoveAlongTrajectory()
        {
            // NOTICE: The airplane has arrived at the end of the tube
            if (_currentTubeIndex >= TrajectoryData.Points.Count - 1)
            {
                Debug.WriteLine("Airplane has arrived at the end of the tube");
                return (default, _pointB, true);
            }

            // Check if there are points in the tube trajectory
            var tubePathPoints = TrajectoryData.Points.ToList();
            if (tubePathPoints.Count < 2)
            {
                Debug.WriteLine("Not enough points in the tube trajectory");
                return (default, AircraftPosition, false);
            }

            // Move to the next tube segment
            _currentTubeIndex++;

            Debug.WriteLine($"Updated _currentTubeIndex: {_currentTubeIndex}");

            // NOTICE: Check if the airplane has arrived at the end of the tube after incrementing index
            if (_currentTubeIndex >= tubePathPoints.Count - 1)
            {
                Debug.WriteLine("Airplane has arrived at the end of the tube");
                // The airplane has arrived at the end of the tube
                return (default, _pointB, true);
            }

            // Update current and next tube points
            var currentTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex].Point3D;
            var nextTubePoint = (Vector3D)tubePathPoints[_currentTubeIndex + 1].Point3D;

            Debug.WriteLine($"New currentTubePoint: {currentTubePoint}");
            Debug.WriteLine($"New nextTubePoint: {nextTubePoint}");

            // Set the airplane position to the next tube point with a slight offset
            AircraftPosition = nextTubePoint + (Normalized(nextTubePoint - currentTubePoint) * PositionOffset);

            Debug.WriteLine($"AircraftPosition: {AircraftPosition}");

            // Calculate the orientation of the airplane
            var aircraftDirections = nextTubePoint - currentTubePoint;

            // The forward and upward direction of the airplane
            var forward = Normalized(aircraftDirections);

            // NOTICE: Calculate the up vector based on the forward vector
            var up = Vector3D.CrossProduct(Vector3D.CrossProduct(forward, new Vector3D(0, 0, 1)), forward);
            up.Normalize();

            Debug.WriteLine($"Forward: {forward}");
            Debug.WriteLine($"Up: {up}");

            // Apply transform
            var planeTransform = GetPlaneTransform(forward, up, AircraftPosition * (1 + HeightOverGround));

            // NOTICE: Reset the index to 0 when reaching the end of the tube
            if (_currentTubeIndex == tubePathPoints.Count - 1)
            {
                _currentTubeIndex = 0;
            }

            // Default return if no conditions are met
            return (planeTransform, AircraftPosition, false);
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

        private static Vector3D Normalized(Vector3D vector)
        {
            return vector / vector.Length;
        }

        private static Transform3D GetPlaneTransform(Vector3D forward, Vector3D up, Vector3D position)
        {
            // // Log the input values
            Debug.WriteLine($"Input forward: {forward}");
            Debug.WriteLine($"Input up: {up}");
            Debug.WriteLine($"Input position: {position}");
            //
            // var v1 = forward;
            // var v3 = up;
            // var v2 = Vector3D.CrossProduct(v1, v3);
            //
            // var matrix3D = new Matrix3D(
            //     v1.X, v1.Y, v1.Z, 0,
            //     v2.X, v2.Y, v2.Z, 0,
            //     v3.X, v3.Y, v3.Z, 0,
            //     0, 0, 0, 1);
            //
            // matrix3D.Scale(new Vector3D(PlaneScale, PlaneScale, PlaneScale));
            //
            // matrix3D.Translate(position);
            //
            // // Log the resulting transformation
            // Debug.WriteLine($"Resulting transform: {matrix3D}");

            //return new MatrixTransform3D(matrix3D);

            forward.Normalize();
            up = Vector3D.CrossProduct(Vector3D.CrossProduct(forward, up), forward);
            up.Normalize();
            
            var right = Vector3D.CrossProduct(forward, up);
            right.Normalize();
            
            var matrix3D = new Matrix3D(
                right.X, right.Y, right.Z, 0,
                up.X, up.Y, up.Z, 0,
                -forward.X, -forward.Y, -forward.Z, 0,
                position.X, position.Y, position.Z, 1
            );

            Debug.WriteLine($"Resulting transform: {matrix3D}");
            return new MatrixTransform3D(matrix3D);
        }
    }
}