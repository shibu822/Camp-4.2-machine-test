using AirlineManagement.Repositories;
using AirlineManagement.Services;
using FlyWithMe.Model;
using FlyWithMe.Repositories;
using FlyWithMe.Utilities;
using System;

namespace AirlineManagement.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repo = new FlightRepository();


        // VIEW ALL FLIGHTS

        public void ShowAllFlights()
        {
            var flights = _repo.GetAllFlights();

            if (flights == null || flights.Count == 0)
            {
                Console.WriteLine("\nNo flights available.");
                return;
            }

            Console.WriteLine("\n--- Flights ---");
            foreach (var f in flights)
            {
                Console.WriteLine(
                    $"{f.FlightDetailId} | {f.FlightName} ({f.FlightCode}) | " +
                    $"{f.FromAirport} → {f.ToAirport} | " +
                    $"Dep: {f.DepartureDateTime:yyyy-MM-dd HH:mm} | " +
                    $"Arr: {f.ArrivalDateTime:yyyy-MM-dd HH:mm}"
                );
            }
        }



        // ADD FLIGHT

        public void AddFlight(int adminId)
        {
            try
            {
                Console.Write("FlightId: ");
                if (!int.TryParse(Console.ReadLine(), out int flightId))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

                Console.Write("Departure AirportId: ");
                if (!int.TryParse(Console.ReadLine(), out int dep))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

                Console.Write("Arrival AirportId: ");
                if (!int.TryParse(Console.ReadLine(), out int arr))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

                if (!ValidationHelper.IsValidAirport(dep, arr))
                    return;

               
                Console.Write("Departure Date (dd-MM-yyyy): ");
                if (!DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd-MM-yyyy",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime depDate))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

               
                Console.Write("Departure Time (HH:mm): ");
                if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan depTime))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

                
                Console.Write("Arrival Date (dd-MM-yyyy): ");
                if (!DateTime.TryParseExact(
                        Console.ReadLine(),
                        "dd-MM-yyyy",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime arrDate))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

              
                Console.Write("Arrival Time (HH:mm): ");
                if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan arrTime))
                {
                    Console.WriteLine("Enter valid data.");
                    return;
                }

                DateTime depDateTime = depDate.Add(depTime);
                DateTime arrDateTime = arrDate.Add(arrTime);

                if (!ValidationHelper.IsValidDateTime(depDateTime, arrDateTime))
                    return;

                _repo.AddFlightDetails(
                    flightId,
                    dep,
                    arr,
                    depDateTime,
                    arrDateTime,
                    adminId
                );

                Console.WriteLine("Flight added successfully!");
            }
            catch
            {
                Console.WriteLine("Enter valid data.");
            }
        }


        // SEARCH FLIGHT

        public void SearchFlight()
        {
            Console.Write("Enter Flight Schedule ID: ");

            if (!int.TryParse(Console.ReadLine(), out int scheduleId))
            {
                Console.WriteLine("Enter valid data.");
                return;
            }

            var flight = _repo.GetFlightByScheduleId(scheduleId);

            if (flight == null)
            {
                Console.WriteLine("Flight schedule not found.");
                return;
            }

            Console.WriteLine("\n--- Flight Schedule Details ---");
            Console.WriteLine(
                $"{flight.FlightDetailId} | {flight.FlightName} ({flight.FlightCode}) | " +
                $"{flight.FromAirport} → {flight.ToAirport} | " +
                $"Dep: {flight.DepartureDateTime:dd-MM-yyyy HH:mm} | " +
                $"Arr: {flight.ArrivalDateTime:dd-MM-yyyy HH:mm}"
            );
        }




        // UPDATE FLIGHT

        public void UpdateFlight()
        {
            
            var flights = _repo.GetAllFlights();

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available to update.");
                return;
            }

            Console.WriteLine("\n--- Available Flights ---");
            foreach (var f in flights)
            {
                Console.WriteLine(
                    $"{f.FlightDetailId} | {f.FlightName} ({f.FlightCode}) | " +
                    $"{f.FromAirport} → {f.ToAirport} | " +
                    $"Dep: {f.DepartureDateTime:yyyy-MM-dd HH:mm} | " +
                    $"Arr: {f.ArrivalDateTime:yyyy-MM-dd HH:mm}"
                );
            }


            Console.Write("\nEnter FlightDetailId to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid FlightDetailId.");
                return;
            }

          
            Console.Write("New Departure Date & Time (yyyy-mm-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newDep))
            {
                Console.WriteLine("Invalid departure date/time.");
                return;
            }

            Console.Write("New Arrival Date & Time (yyyy-mm-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newArr))
            {
                Console.WriteLine("Invalid arrival date/time.");
                return;
            }

           
            if (!ValidationHelper.IsValidDateTime(newDep, newArr))
                return;

            // Step 5: Confirm update
            Console.Write("Are you sure you want to update this flight? (Y/N): ");
            string confirm = Console.ReadLine();

            if (!confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Update cancelled.");
                return;
            }

         
            _repo.UpdateFlightDateTime(id, newDep, newArr);
            Console.WriteLine("Flight updated successfully!");
        }



        // DELETE FLIGHT

        public void DeleteFlight()
        {
           
            var flights = _repo.GetAllFlights();

            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available to delete.");
                return;
            }

            Console.WriteLine("\n--- Available Flights ---");
            foreach (var f in flights)
            {
                Console.WriteLine(
                    $"{f.FlightDetailId} | {f.FlightName} ({f.FlightCode}) | " +
                    $"{f.FromAirport} → {f.ToAirport} | " +
                    $"Dep: {f.DepartureDateTime:yyyy-MM-dd HH:mm}"
                );
            }

            Console.Write("\nEnter FlightDetailId to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid FlightDetailId.");
                return;
            }

     
            Console.Write("Are you sure you want to delete this flight? (Y/N): ");
            string confirm = Console.ReadLine();

            if (!confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Delete cancelled.");
                return;
            }

       
            _repo.DeleteFlight(id);
            Console.WriteLine("Flight deleted successfully!");
        }

    }
}
