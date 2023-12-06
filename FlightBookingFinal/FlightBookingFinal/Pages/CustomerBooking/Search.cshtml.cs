using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.CustomerBooking
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        public string DepartureAirport { get; set; }

        [BindProperty]
        public string DestinationAirport { get; set; }

        [BindProperty]
        public DateTime DepartureDate { get; set; }

        public List<string> DepartureAirports { get; set; } = new List<string>();
        public List<string> DestinationAirports { get; set; } = new List<string>();
        public List<FlightSchedule> Flights { get; set; } = new List<FlightSchedule>();

        public IActionResult OnGet()
        {
            LoadAirports();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadAirports();
                return Page();
            }

            LoadFlights();
            return Page();
        }

        private void LoadAirports()
        {
            // Populate DepartureAirports and DestinationAirports from the database
            // Use your database connection and queries here
            // Example:
            DepartureAirports = GetAirportsFromDatabase();
            DestinationAirports = GetAirportsFromDatabase();
        }

        private List<string> GetAirportsFromDatabase()
        {
            // Sample code to retrieve airport names from the database
            List<string> airports = new List<string>();

            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Name FROM Airports";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                airports.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return airports;
        }

        private void LoadFlights()
        {
            
            Flights = GetFlightsFromDatabase(DepartureAirport, DestinationAirport, DepartureDate);
        }

        private List<FlightSchedule> GetFlightsFromDatabase(string departureAirport, string destinationAirport, DateTime departureDate)
        {
            // Sample code to retrieve flights from the database based on user input
            List<FlightSchedule> flights = new List<FlightSchedule>();

            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM FlightSchedules WHERE DepartureAirportName = @DepartureAirport AND DestinationAirportName = @DestinationAirport AND DepartureTime >= @DepartureDate";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@DepartureAirport", departureAirport);
                        cmd.Parameters.AddWithValue("@DestinationAirport", destinationAirport);
                        cmd.Parameters.AddWithValue("@DepartureDate", departureDate);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                flights.Add(new FlightSchedule
                                {
                                    FlightName = reader.GetString(1),
                                    DepartureAirport = reader.GetString(2),
                                    DestinationAirport = reader.GetString(3),
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

            return flights;
        }
    }
}
