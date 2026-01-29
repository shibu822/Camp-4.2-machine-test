namespace FlyWithMe.Model
{
    public class Flight
    {
        public int FlightDetailId { get; set; }

        public string FlightName { get; set; }
        public string FlightCode { get; set; }

        public string FromAirport { get; set; }
        public string ToAirport { get; set; }

        // Proper DateTime properties
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
