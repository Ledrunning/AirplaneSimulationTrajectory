using AirplaneSimulationTrajectory.Model;
using System;
using System.Windows.Media.Media3D;

namespace AirplaneSimulationTrajectory.Contracts
{
    public interface IAircraftService
    {
        Vector3D AircraftPosition { get; set; }
        void SetPlanePath(Vector3D from, Vector3D to);
        (Transform3D planeTransform, Vector3D secondPosition, bool resetTimer) UpdatePosition();
        Vector3D MovementCalculation(DateTime now, DateTime juneSolstice);
        RoutePointModel AddRoutePoints(double latitude, double longitude);

        Point3DCollection AddTubeRoutePoints();
    }
}