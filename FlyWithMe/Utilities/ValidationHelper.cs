using System;

namespace FlyWithMe.Utilities
{
    public static class ValidationHelper
    {
        public static bool IsValidAirport(int dep, int arr)
        {
            if (dep == arr)
            {
                Console.WriteLine("Departure and Arrival airports cannot be same.");
                return false;
            }
            return true;
        }

        public static bool IsValidDateTime(DateTime dep, DateTime arr)
        {
            if (arr <= dep)
            {
                Console.WriteLine("Arrival date/time must be after departure.");
                return false;
            }
            return true;
        }

        public static bool IsNotEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine($"{fieldName} cannot be empty.");
                return false;
            }
            return true;
        }
    }
}
