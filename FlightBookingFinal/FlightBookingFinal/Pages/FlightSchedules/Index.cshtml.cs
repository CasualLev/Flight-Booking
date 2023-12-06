using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.FlightSchedules
{
    public class IndexModel : PageModel
    {
        public List<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();

        public void OnGet()
        {
            LoadFlightSchedules();
        }

        private void LoadFlightSchedules()
        {
            FlightSchedules.Clear();

            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM FlightSchedules";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FlightSchedules.Add(new FlightSchedule
                                {
                                    RouteNumber = reader.GetString(0),
                                    FlightName = reader.GetString(1),
                                    DepartureAirport = reader.GetString(2),
                                    DestinationAirport = reader.GetString(3),
                                    EconomySeats = reader.GetInt32(4),
                                    EconomyPrice = reader.GetDecimal(5),
                                    BusinessSeats = reader.GetInt32(6),
                                    BusinessPrice = reader.GetDecimal(7),
                                    DepartureTime = reader.GetDateTime(8),
                                    ArrivalTime = reader.GetDateTime(9)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
