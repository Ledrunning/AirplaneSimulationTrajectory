using System;
using System.Windows.Media.Media3D;

namespace AirplaneSimulationTrajectory.Contracts
{
    public interface IAircraftService
    {
        Vector3D AircraftPosition { get; set; }
        (Vector3D pointA, Vector3D pointB) SetPlanePath(Vector3D from, Vector3D to);   
        (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdateAircraftPosition();
        Vector3D MovementCalculation(DateTime now, DateTime juneSolstice);

        Point3D NormalizePoint(Point3D point);
    }
}