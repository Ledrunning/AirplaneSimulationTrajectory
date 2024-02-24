using AirplaneSimulationTrajectory.Contracts;
using System;
using CommonConfiguration.Configuration.Model;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class FlightInfoViewModel : BaseViewModel, IFlightInfoViewModel
    {
        private readonly Settings _settings;
        private string _coordinates;
        private string _currentTime;
        private string _temperature;
        private string _totalFlightTime;
        private string _flightLength;
        private string _title;

        public FlightInfoViewModel(Settings settings)
        {
            _settings = settings;
        }

        public string Title
        {
            get => _title;
            set => SetField(ref _title, value);
        }

        public string CurrentTime
        {
            get => _currentTime;
            set => SetField(ref _currentTime, value);
        }

        public string TotalFlightTime 
        {
            get => _totalFlightTime;
            set => SetField(ref _totalFlightTime, value);
        }

        public string FlightLength
        {
            get => _flightLength;
            set => SetField(ref _flightLength, value);
        }

        public string Temperature
        {
            get => _temperature;
            set => SetField(ref _temperature, value);
        }

        public string Coordinates
        {
            get => _coordinates;
            set => SetField(ref _coordinates, value);
        }

        public void InitializeData()
        {
            CurrentTime = null;
            Coordinates = "-/-";
            Temperature = "-/-";
            FlightLength = $"{_settings.FlightInformation.FlightLength} Km";
            TotalFlightTime = $"{_settings.FlightInformation.TotalFlightTime} h";
        }

        private static int NumberGenerator(int firstRangeNumber, int secondRangeNumber)
        {
            var random = new Random();
            var randomNumber = random.Next(firstRangeNumber, secondRangeNumber);
            return randomNumber;
        }

        public void UpdateData(double latitude, double longitude)
        {
            CurrentTime = DateTime.Now.ToShortTimeString();
            Coordinates = $"{latitude:0.00} {longitude:0.00}";
            Temperature = $"{NumberGenerator(-15, -10)}°C";
        }

        public void ClearFields()
        {
            CurrentTime = string.Empty;
            Coordinates = string.Empty;
            Temperature = string.Empty;
            FlightLength = string.Empty;
            TotalFlightTime = string.Empty;
        }
    }
}