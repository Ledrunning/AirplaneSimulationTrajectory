using System.Windows.Media.Media3D;

namespace AirplaneSimulationTrajectory.Services
{
    internal class AircraftService
    {
        private readonly double EarthRadius = 1; //Earth radius
        private readonly double PlaneScale = 1; //plane scale
        private double deltaTime = 0.001; //delta time
        private double HeightOverGround = 0.02;
        private double MinDistance = 0.01f; //current plane pos
        private Vector3D PointA; //from point
        private Vector3D PointB; //to point
        public Vector3D PlanePosition { get; set; } //current plane pos

        public void SetPlanePath(Vector3D from, Vector3D to)
        {
            PointA = Normalized(from) * EarthRadius;
            PointB = Normalized(to) * EarthRadius;
            PlanePosition = PointA;
        }

        private static Vector3D Normalized(Vector3D vector)
        {
            return vector / vector.Length;
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
    }
}