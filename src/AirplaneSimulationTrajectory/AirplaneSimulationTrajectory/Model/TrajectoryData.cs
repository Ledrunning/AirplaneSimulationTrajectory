using System.Collections.Generic;
using AirplaneSimulationTrajectory.Constants;
using AirplaneSimulationTrajectory.Helpers;

namespace AirplaneSimulationTrajectory.Model
{
    public static class TrajectoryData
    {
        private static CustomLinkedList<RoutePointModel> _points;
        private const bool Reverse = false;
        
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
                new RoutePointModel(45.046154, 7.728707,    EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(45.370518, 9.584115,    EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(46.873207, 14.230472,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(46.854345, 20.738590,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(47.398169, 26.058499,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(45.061323, 33.610464,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(47.124333, 40.235870,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(47.522226, 53.926108,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(46.451989, 69.806335,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(45.460486, 83.548558,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(45.048881, 96.773142,   EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(44.705248, 115.137483,  EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(45.300619, 129.645888,  EarthConstants.RadiusDelta + EarthConstants.EarthRadius),
                new RoutePointModel(45.850283, 154.110614,  EarthConstants.RadiusDelta + EarthConstants.EarthRadius),

                new RoutePointModel(58.099162, 159.776759,  EarthConstants.EarthRadius),
                new RoutePointModel(63.525080, 146.474241,  EarthConstants.EarthRadius),
                new RoutePointModel(67.991231, 141.280312,  EarthConstants.EarthRadius),
                new RoutePointModel(67.199425, 161.573944,  EarthConstants.EarthRadius),
                new RoutePointModel(47.202575, 163.115150,  EarthConstants.EarthRadius),

                new RoutePointModel(46.290879, 172.711484,  EarthConstants.EarthRadius),
                new RoutePointModel(46.642285, -170.691651, EarthConstants.EarthRadius),
                new RoutePointModel(50.038821, -145.903645, EarthConstants.EarthRadius),
                new RoutePointModel(52.149747, -118.421554, EarthConstants.EarthRadius),
                new RoutePointModel(51.006280, -99.683388,  EarthConstants.EarthRadius),
                new RoutePointModel(49.040722, -77.101195,  EarthConstants.EarthRadius),
                new RoutePointModel(48.713327, -60.329267,  EarthConstants.EarthRadius),
                new RoutePointModel(49.081636, -43.757233,  EarthConstants.EarthRadius),
                new RoutePointModel(49.086151, -28.884012,  EarthConstants.EarthRadius),
                new RoutePointModel(48.134069, -14.950992,  EarthConstants.EarthRadius),
                new RoutePointModel(46.562876, -1.585133,   EarthConstants.EarthRadius),
                new RoutePointModel(45.813983, 3.566655,    EarthConstants.EarthRadius),
                new RoutePointModel(45.396551, 6.993254,    EarthConstants.EarthRadius),
            };
        }
    }
}