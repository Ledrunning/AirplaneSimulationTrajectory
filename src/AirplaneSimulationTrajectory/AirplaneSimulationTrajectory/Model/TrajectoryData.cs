using System.Collections.Generic;
using AirplaneSimulationTrajectory.Helpers;

namespace AirplaneSimulationTrajectory.Model
{
    public static class TrajectoryData
    {
        private static CustomLinkedList<RoutePointModel> _points;
        private const bool Reverse = false;
        private const int RadiusDelta = 100;
        private const int EarthSphereRadius = 6371;
        public static CustomLinkedList<RoutePointModel> Points
        {
            get
            {
                if (_points != null) return _points;
                _points = new CustomLinkedList<RoutePointModel>(InitializePoints());
                if (Reverse)
                {
                    _points.Reverse();
                }

                return _points;
            }
        }

        private static IEnumerable<RoutePointModel> InitializePoints()
        {
            return new[]
            {
                new RoutePointModel(45.046154, 7.728707,    RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.370518, 9.584115,    RadiusDelta + EarthSphereRadius),
                new RoutePointModel(46.873207, 14.230472,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(46.854345, 20.738590,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(47.398169, 26.058499,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.061323, 33.610464,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(47.124333, 40.235870,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(47.522226, 53.926108,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(46.451989, 69.806335,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.460486, 83.548558,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.048881, 96.773142,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(44.705248, 115.137483,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.300619, 129.645888,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.850283, 154.110614,  RadiusDelta + EarthSphereRadius),

                new RoutePointModel(58.099162, 159.776759,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(63.525080, 146.474241,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(67.991231, 141.280312,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(67.199425, 161.573944,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(47.202575, 163.115150,  RadiusDelta + EarthSphereRadius),
                                                            
                new RoutePointModel(46.290879, 172.711484,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(46.642285, -170.691651, RadiusDelta + EarthSphereRadius),
                new RoutePointModel(50.038821, -145.903645, RadiusDelta + EarthSphereRadius),
                new RoutePointModel(52.149747, -118.421554, RadiusDelta + EarthSphereRadius),
                new RoutePointModel(51.006280, -99.683388,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(49.040722, -77.101195,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(48.713327, -60.329267,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(49.081636, -43.757233,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(49.086151, -28.884012,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(48.134069, -14.950992,  RadiusDelta + EarthSphereRadius),
                new RoutePointModel(46.562876, -1.585133,   RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.813983, 3.566655,    RadiusDelta + EarthSphereRadius),
                new RoutePointModel(45.396551, 6.993254,    RadiusDelta + EarthSphereRadius),
            };
        }
    }
}