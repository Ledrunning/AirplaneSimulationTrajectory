using System.Xml.Serialization;

namespace CommonConfiguration.Configuration.Model
{
    [XmlRoot(ElementName = "routeCoordinates")]
    public class RouteCoordinates
    {
        [XmlElement(ElementName = "startPointLat")]
        public double StartPointLat { get; set; }

        [XmlElement(ElementName = "startPointLon")]
        public double StartPointLon { get; set; }

        [XmlElement(ElementName = "endPointLat")]
        public double EndPointLat { get; set; }

        [XmlElement(ElementName = "endPointLon")]
        public double EndPointLon { get; set; }
    }

    [XmlRoot(ElementName = "tubeConfiguration")]
    public class TubeConfiguration
    {
        [XmlElement(ElementName = "diameter")] public double Diameter { get; set; }

        [XmlElement(ElementName = "thetaDiv")] public int ThetaDiv { get; set; }

        [XmlElement(ElementName = "opacity")] public double Opacity { get; set; }

        [XmlElement(ElementName = "color")] public string Color { get; set; }
    }

    [XmlRoot(ElementName = "flightInformation")]
    public class FlightInformation
    {
        [XmlElement(ElementName = "flightLength")]
        public int FlightLength { get; set; }

        [XmlElement(ElementName = "totalFlightTime")]
        public int TotalFlightTime { get; set; }
    }

    [XmlRoot(ElementName = "settings")]
    public class Settings
    {
        [XmlElement(ElementName = "timerSpeedMs")]
        public int TimerSpeedMs { get; set; }

        [XmlElement(ElementName = "cloudsOpacity")]
        public double CloudsOpacity { get; set; }

        [XmlElement(ElementName = "routeCoordinates")]
        public RouteCoordinates RouteCoordinates { get; set; }

        [XmlElement(ElementName = "tubeConfiguration")]
        public TubeConfiguration TubeConfiguration { get; set; }

        [XmlElement(ElementName = "flightInformation")]
        public FlightInformation FlightInformation { get; set; }
    }
}