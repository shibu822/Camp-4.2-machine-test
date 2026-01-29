namespace AirlineManagement.Services
{
    public interface IFlightService
    {
        // CREATE
        void AddFlight(int adminId);

        // READ
        void ShowAllFlights();

        // SEARCH
        void SearchFlight();

        // UPDATE
        void UpdateFlight();

        // DELETE
        void DeleteFlight();
    }
}
