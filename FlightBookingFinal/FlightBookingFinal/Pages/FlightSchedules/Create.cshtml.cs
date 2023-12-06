using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.FlightSchedules
{
    public class CreateModel : PageModel
    {
         public FlightSchedule fSchedule = new FlightSchedule();
        public List<FlightSchedule> fSchedules = new List<FlightSchedule>();
        public List<string> listairlines = new List<string>();
        public List<string> listairports = new List<string>();

        public void OnGet()
        {
            LoadAirlines();
            LoadAirports();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadAirlines();
                LoadAirports();
                return Page();
            }

            try
            {
                fSchedule.RouteNumber = Request.Form["RouteNumber"];
                fSchedule.FlightName = Request.Form["FlightName"];
                fSchedule.DepartureAirport = Request.Form["DepartureAirport"];
                fSchedule.DestinationAirport = Request.Form["DestinationAirport"];
                fSchedule.EconomySeats = int.Parse(Request.Form["EconomySeats"]);
                fSchedule.EconomyPrice = decimal.Parse(Request.Form["EconomyPrice"]);
                fSchedule.BusinessSeats = int.Parse(Request.Form["BusinessSeats"]);
                fSchedule.BusinessPrice = decimal.Parse(Request.Form["BusinessPrice"]);
                fSchedule.DepartureTime = DateTime.Parse(Request.Form["DepartureTime"]);
                fSchedule.ArrivalTime = DateTime.Parse(Request.Form["ArrivalTime"]);

                // Save the flight schedule to the database
                SaveFlightSchedule(fSchedule);

                // Reload the list of flight schedules after successful creation
                LoadFlightSchedules();

                return RedirectToPage("/FlightSchedules/Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return Page();
            }
        }

        private void SaveFlightSchedule(FlightSchedule flightSchedule)
        {
            string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = "INSERT INTO FlightSchedules (FlightRouteNumber, FlightName, DepartureAirportName, DestinationAirportName, EconomyClassSeats, EconomyClassPrice, BusinessClassSeats, BusinessClassPrice, DepartureTime, ArrivalTime) " +
                                  "VALUES (@RouteNumber, @FlightName, @DepartureAirport, @DestinationAirport, @EconomySeats, @EconomyPrice, @BusinessSeats, @BusinessPrice, @DepartureTime, @ArrivalTime)";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@RouteNumber", flightSchedule.RouteNumber);
                    cmd.Parameters.AddWithValue("@FlightName", flightSchedule.FlightName);
                    cmd.Parameters.AddWithValue("@DepartureAirport", flightSchedule.DepartureAirport);
                    cmd.Parameters.AddWithValue("@DestinationAirport", flightSchedule.DestinationAirport);
                    cmd.Parameters.AddWithValue("@EconomySeats", flightSchedule.EconomySeats);
                    cmd.Parameters.AddWithValue("@EconomyPrice", flightSchedule.EconomyPrice);
                    cmd.Parameters.AddWithValue("@BusinessSeats", flightSchedule.BusinessSeats);
                    cmd.Parameters.AddWithValue("@BusinessPrice", flightSchedule.BusinessPrice);
                    cmd.Parameters.AddWithValue("@DepartureTime", flightSchedule.DepartureTime);
                    cmd.Parameters.AddWithValue("@ArrivalTime", flightSchedule.ArrivalTime);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadFlightSchedules()
        {
            fSchedules.Clear();

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
                                fSchedules.Add(new FlightSchedule
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

        private void LoadAirlines()
        {
            listairlines.Clear();

            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Name FROM Airlines";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listairlines.Add(reader.GetString(0));
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

        private void LoadAirports()
        {
            listairports.Clear();

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
                                listairports.Add(reader.GetString(0));
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
