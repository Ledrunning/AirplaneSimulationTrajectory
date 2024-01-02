using AirplaneSimulationTrajectory.Contracts;
using AirplaneSimulationTrajectory.ViewModel;

namespace AirplaneSimulationTrajectory.View.Controls.FlightInfo
{
    public class FlightInfoControlDataContext : BaseViewModel, IFlightInfoControlDataContext
    {
        private string _coordinates;
        private string _currentTime;
        private string _title;

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

        public string TotalFlightTime { get; set; }
        public string FlightLength { get; set; }
        public string Temperature { get; set; }

        public string Coordinates
        {
            get => _coordinates;
            set => SetField(ref _coordinates, value);
        }

        public void InitializeData()
        {
            CurrentTime = null;
            Coordinates = "test";
            Temperature = "-35*";
            FlightLength = "5400";
            TotalFlightTime = "14h";
        }
    }
}