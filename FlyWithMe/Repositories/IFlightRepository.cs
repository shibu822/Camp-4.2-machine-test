using FlyWithMe.Model;
using System;
using System.Collections.Generic;

namespace AirlineManagement.Repositories
{
    public interface IFlightRepository
    {
        // READ
        List<Flight> GetAllFlights();

        // CREATE
        void AddFlightDetails(
            int flightId,
            int depAirportId,
            int arrAirportId,
            DateTime depDateTime,
            DateTime arrDateTime,
            int adminId
        );

        // SEARCH
        Flight GetFlightByScheduleId(int flightDetailId);


        // UPDATE
        void UpdateFlightDateTime(
            int flightDetailId,
            DateTime newDepartureDateTime,
            DateTime newArrivalDateTime
        );

        // DELETE
        void DeleteFlight(int flightDetailId);
    }
}
