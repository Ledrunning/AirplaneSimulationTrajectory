namespace AirplaneSimulationTrajectory.Contracts
{
    public interface IFlightInfoControlDataContext
    {
        string Title { get; set; }
        string CurrentTime { get; set; }
        string TotalFlightTime { get; set; }
        string FlightLength { get; set; }
        string Temperature { get; set; }
        string Coordinates { get; set; }

        void InitializeData();
    }
}