using AirplaneSimulationTrajectory.ViewModel;

namespace AirplaneSimulationTrajectory.View.Controls.FlightInfo
{
    public class FlightInfoControlDataContext : BaseViewModel
    {
        private string _title;
        private string _coordinates;
        private string _currentTime;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        public string TotalFlightTime { get; set; }
        public string FlightLength { get; set; }
        public string Temperature { get; set; }

        public string Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                OnPropertyChanged(nameof(Coordinates));
            }
        }
    }
}