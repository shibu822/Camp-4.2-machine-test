using AirlineManagement.Services;
using System;

namespace AirlineManagement
{
    class Program
    {
        static void Main()
        {
            IAuthService authService = new AuthService();

            Console.Write("Username: ");
            string user = Console.ReadLine();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            int adminId = authService.Login(user, pass);

            if (adminId == -1)
            {
                Console.WriteLine("Invalid login");
                return;
            }

            Console.WriteLine("Login successful");

            IFlightService flightService = new FlightService();

            while (true)
            {
                Console.WriteLine("\n===== FLIGHT MANAGEMENT MENU =====");
                Console.WriteLine("1. Add Flight Shedule");
                Console.WriteLine("2. View Flights Shedule's");
                Console.WriteLine("3. Search Flight Shedule's");
                Console.WriteLine("4. Update Flight Shedule's");
                Console.WriteLine("5. Delete Flight Shedule's");
                Console.WriteLine("6. Exit");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        flightService.AddFlight(adminId);
                        break;

                    case 2:
                        flightService.ShowAllFlights();
                        break;

                    case 3:
                        flightService.SearchFlight();
                        break;

                    case 4:
                        flightService.UpdateFlight();
                        break;

                    case 5:
                        flightService.DeleteFlight();
                        break;

                    case 6:
                        Console.WriteLine("Exiting application...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}

                
                
