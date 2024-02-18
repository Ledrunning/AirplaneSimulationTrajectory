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

        private SettingsRouteCoordinates _routeCoordinatesField;

        private byte _timerSpeedMsField;

        private SettingsTubeConfiguration _tubeConfigurationField;

        /// <remarks />
        public byte TimerSpeedMs
        {
            get => _timerSpeedMsField;
            set => _timerSpeedMsField = value;
        }

        /// <remarks />
        public SettingsRouteCoordinates RouteCoordinates
        {
            get => _routeCoordinatesField;
            set => _routeCoordinatesField = value;
        }

        /// <remarks />
        public SettingsTubeConfiguration TubeConfiguration
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
    public class SettingsRouteCoordinates
    {
        private decimal _endPointLatField;

        private decimal _endPointLonField;

        private decimal _startPointLatField;

        private decimal _startPointLonField;

        /// <remarks />
        public decimal StartPointLat
        {
            get => _startPointLatField;
            set => _startPointLatField = value;
        }

        /// <remarks />
        public decimal StartPointLon
        {
            get => _startPointLonField;
            set => _startPointLonField = value;
        }

        /// <remarks />
        public decimal EndPointLat
        {
            get => _endPointLatField;
            set => _endPointLatField = value;
        }

        /// <remarks />
        public decimal EndPointLon
        {
            get => _endPointLonField;
            set => _endPointLonField = value;
        }
    }

    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class SettingsTubeConfiguration
    {
        private string _colorField;

        private decimal _diametrField;

        private decimal _opacityField;

        private byte _thetaDivField;

        /// <remarks />
        public decimal Diametr
        {
            get => _diametrField;
            set => _diametrField = value;
        }

        /// <remarks />
        public byte ThetaDiv
        {
            get => _thetaDivField;
            set => _thetaDivField = value;
        }

        /// <remarks />
        public decimal Opacity
        {
            get => _opacityField;
            set => _opacityField = value;
        }

        /// <remarks />
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
        private ushort _flightLengthField;

        private byte _totalFlightTimeField;

        /// <remarks />
        public ushort FlightLength
        {
            get => _flightLengthField;
            set => _flightLengthField = value;
        }

        /// <remarks />
        public byte TotalFlightTime
        {
            get => _totalFlightTimeField;
            set => _totalFlightTimeField = value;
        }
    }
}