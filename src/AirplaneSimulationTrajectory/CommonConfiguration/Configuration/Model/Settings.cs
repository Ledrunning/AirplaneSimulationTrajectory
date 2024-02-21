using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CommonConfiguration.Configuration.Model
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "settings")]
    public class Settings
    {
        private SettingsFlightInformation _flightInformationField;

        private RouteCoordinates _routeCoordinatesField;

        private int _timerSpeedMsField;

        private TubeConfiguration _tubeConfigurationField;

        [XmlElement("timerSpeedMs")]
        public int TimerSpeedMs
        {
            get => _timerSpeedMsField;
            set => _timerSpeedMsField = value;
        }

        /// <remarks />
        public RouteCoordinates RouteCoordinates
        {
            get => _routeCoordinatesField;
            set => _routeCoordinatesField = value;
        }

        /// <remarks />
        public TubeConfiguration TubeConfiguration
        {
            get => _tubeConfigurationField;
            set => _tubeConfigurationField = value;
        }

        /// <remarks />
        public SettingsFlightInformation FlightInformation
        {
            get => _flightInformationField;
            set => _flightInformationField = value;
        }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class RouteCoordinates
    {
        private double _endPointLatField;
        private double _endPointLonField;
        private double _startPointLatField;
        private double _startPointLonField;

        [XmlElement("startPointLat")]
        public double StartPointLat
        {
            get => _startPointLatField;
            set => _startPointLatField = value;
        }

        [XmlElement("startPointLon")]
        public double StartPointLon
        {
            get => _startPointLonField;
            set => _startPointLonField = value;
        }

        [XmlElement("endPointLat")]
        public double EndPointLat
        {
            get => _endPointLatField;
            set => _endPointLatField = value;
        }

        [XmlElement("endPointLon")]
        public double EndPointLon
        {
            get => _endPointLonField;
            set => _endPointLonField = value;
        }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class TubeConfiguration
    {
        private string _colorField;

        private double _diameterField;

        private double _opacityField;

        private int _thetaDivField;

        [XmlElement("diameter")]
        public double Diameter
        {
            get => _diameterField;
            set => _diameterField = value;
        }

        [XmlElement("thetaDiv")]
        public int ThetaDiv
        {
            get => _thetaDivField;
            set => _thetaDivField = value;
        }

        [XmlElement("opacity")]
        public double Opacity
        {
            get => _opacityField;
            set => _opacityField = value;
        }

        [XmlElement("color")]
        public string Color
        {
            get => _colorField;
            set => _colorField = value;
        }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SettingsFlightInformation
    {
        private int _flightLengthField;

        private int _totalFlightTimeField;

        [XmlElement("flightLength")]
        public int FlightLength
        {
            get => _flightLengthField;
            set => _flightLengthField = value;
        }

        [XmlElement("tlightLength")]
        public int TotalFlightTime
        {
            get => _totalFlightTimeField;
            set => _totalFlightTimeField = value;
        }
    }
}