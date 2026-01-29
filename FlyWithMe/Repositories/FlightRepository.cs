using FlyWithMe.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using AirlineManagement.Repositories;

namespace FlyWithMe.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly string _connStr =
            ConfigurationManager.ConnectionStrings["FlyWithMeDB"]?.ConnectionString
            ?? throw new InvalidOperationException(
                "Connection string 'FlyWithMeDB' not found.");


        // VIEW ALL FLIGHTS
         
        public List<Flight> GetAllFlights()
        {
            var flights = new List<Flight>();

            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(@"
                SELECT fd.FlightDetailId, f.FlightName, f.FlightCode,
                       a1.AirportName AS FromAirport,
                       a2.AirportName AS ToAirport,
                       fd.DepDate, fd.DepTime,
                       fd.ArrDate, fd.ArrTime
                FROM FlightDetails fd
                JOIN Flight f ON fd.FlightId = f.FlightId
                JOIN Airport a1 ON fd.DepAirportId = a1.AirportId
                JOIN Airport a2 ON fd.ArrAirportId = a2.AirportId", con);

            con.Open();
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                flights.Add(new Flight
                {
                    FlightDetailId = (int)dr["FlightDetailId"],
                    FlightName = dr["FlightName"].ToString(),
                    FlightCode = dr["FlightCode"].ToString(),
                    FromAirport = dr["FromAirport"].ToString(),
                    ToAirport = dr["ToAirport"].ToString(),

                   
                    DepartureDateTime =
                        ((DateTime)dr["DepDate"])
                            .Add((TimeSpan)dr["DepTime"]),

                    ArrivalDateTime =
                        ((DateTime)dr["ArrDate"])
                            .Add((TimeSpan)dr["ArrTime"])
                });
            }

            return flights;
        }

        
        // ADD FLIGHT
     
        public void AddFlightDetails(
            int flightId,
            int depAirportId,
            int arrAirportId,
            DateTime depDateTime,
            DateTime arrDateTime,
            int adminId)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(@"
                INSERT INTO FlightDetails
                (FlightId, DepAirportId, ArrAirportId,
                 DepDate, DepTime, ArrDate, ArrTime, AdminId)
                VALUES (@f,@d,@a,@dd,@dt,@ad,@at,@admin)", con);

            cmd.Parameters.AddWithValue("@f", flightId);
            cmd.Parameters.AddWithValue("@d", depAirportId);
            cmd.Parameters.AddWithValue("@a", arrAirportId);
            cmd.Parameters.AddWithValue("@dd", depDateTime.Date);
            cmd.Parameters.AddWithValue("@dt", depDateTime.TimeOfDay);
            cmd.Parameters.AddWithValue("@ad", arrDateTime.Date);
            cmd.Parameters.AddWithValue("@at", arrDateTime.TimeOfDay);
            cmd.Parameters.AddWithValue("@admin", adminId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // SEARCH BY FLIGHT schedule id
   
        public Flight GetFlightByScheduleId(int flightDetailId)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(@"
        SELECT fd.FlightDetailId, f.FlightName, f.FlightCode,
               a1.AirportName AS FromAirport,
               a2.AirportName AS ToAirport,
               fd.DepDate, fd.DepTime,
               fd.ArrDate, fd.ArrTime
        FROM FlightDetails fd
        JOIN Flight f ON fd.FlightId = f.FlightId
        JOIN Airport a1 ON fd.DepAirportId = a1.AirportId
        JOIN Airport a2 ON fd.ArrAirportId = a2.AirportId
        WHERE fd.FlightDetailId = @id", con);

            cmd.Parameters.AddWithValue("@id", flightDetailId);

            con.Open();
            using var dr = cmd.ExecuteReader();

            if (!dr.Read())
                return null;

            return new Flight
            {
                FlightDetailId = (int)dr["FlightDetailId"],
                FlightName = dr["FlightName"].ToString(),
                FlightCode = dr["FlightCode"].ToString(),
                FromAirport = dr["FromAirport"].ToString(),
                ToAirport = dr["ToAirport"].ToString(),
                DepartureDateTime =
                    ((DateTime)dr["DepDate"]).Add((TimeSpan)dr["DepTime"]),
                ArrivalDateTime =
                    ((DateTime)dr["ArrDate"]).Add((TimeSpan)dr["ArrTime"])
            };
        }





        // UPDATE FLIGHT DATE & TIME

        public void UpdateFlightDateTime(
            int flightDetailId,
            DateTime newDepartureDateTime,
            DateTime newArrivalDateTime)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(@"
                UPDATE FlightDetails
                SET DepDate = @dd,
                    DepTime = @dt,
                    ArrDate = @ad,
                    ArrTime = @at
                WHERE FlightDetailId = @id", con);

            cmd.Parameters.AddWithValue("@dd", newDepartureDateTime.Date);
            cmd.Parameters.AddWithValue("@dt", newDepartureDateTime.TimeOfDay);
            cmd.Parameters.AddWithValue("@ad", newArrivalDateTime.Date);
            cmd.Parameters.AddWithValue("@at", newArrivalDateTime.TimeOfDay);
            cmd.Parameters.AddWithValue("@id", flightDetailId);

            con.Open();
            cmd.ExecuteNonQuery();
        }


        // DELETE FLIGHT

        public void DeleteFlight(int flightDetailId)
        {
            using var con = new SqlConnection(_connStr);
            using var cmd = new SqlCommand(
                "DELETE FROM FlightDetails WHERE FlightDetailId = @id", con);

            cmd.Parameters.AddWithValue("@id", flightDetailId);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
