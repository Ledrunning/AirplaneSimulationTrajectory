namespace AirplaneSimulationTrajectory.Contracts
{
    public interface IFlightInfoViewModel
    {
        string Title { get; set; }
        string CurrentTime { get; set; }
        string TotalFlightTime { get; set; }
        string FlightLength { get; set; }
        string Temperature { get; set; }
        string Coordinates { get; set; }

        void InitializeData();

        void UpdateData(double latitude, double longitude);

        void ClearFields();
    }
}