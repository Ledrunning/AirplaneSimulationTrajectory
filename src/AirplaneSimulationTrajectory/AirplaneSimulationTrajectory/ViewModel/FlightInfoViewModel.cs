using AirplaneSimulationTrajectory.Contracts;

namespace AirplaneSimulationTrajectory.ViewModel
{
    public class FlightInfoViewModel : BaseViewModel, IFlightInfoViewModel
    {
        private string _coordinates;
        private string _currentTime;
        private string _temperature;
        private string _title;

        public FlightInfoViewModel()
        {
            InitializeData();
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

        public string TotalFlightTime { get; set; }
        public string FlightLength { get; set; }

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
            Coordinates = "test";
            Temperature = "-35*";
            FlightLength = "5400";
            TotalFlightTime = "14h";
        }
    }
}