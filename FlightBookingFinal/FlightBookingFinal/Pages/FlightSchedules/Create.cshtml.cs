using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.FlightSchedules
{
    public class CreateModel : PageModel
    {
        public FlightSchedule fSchedule = new FlightSchedule();
        public String errorMessage = "";
        public String successMessage = "";
        public List<airlineInfo> listairlines = new List<airlineInfo>();
        public List<airportInfo> listairports = new List<airportInfo>();

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadFlightSchedules();
                LoadAirlines();
                LoadAirports();
                
            }
            try
            {

                fSchedule.RouteNumber = Request.Form["RouteNumber"];
                fSchedule.FlightName = Request.Form["FlightName"];
                fSchedule.DepartureAirport = Request.Form["DepartureAirport"];
                fSchedule.DestinationAirport = Request.Form["DestinationAirport"];
                fSchedule.EconomySeats = int.Parse(Request.Form["EconomySeats"]);
                fSchedule.EconomyPrice = int.Parse(Request.Form["EconomyPrice"]);
                fSchedule.BusinessSeats = int.Parse(Request.Form["BusinessSeats"]);
                fSchedule.BusinessPrice = int.Parse(Request.Form["BusinessPrice"]);
                fSchedule.DepartureTime = DateTime.Parse(Request.Form["DepartureTime"]);
                fSchedule.ArrivalTime = DateTime.Parse(Request.Form["ArrivalTime"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQL Exception:  {ex.Message}");
            }
        }

        private void LoadFlightSchedules()
        {
            fSchedule.Clear();

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
                                fSchedule.Add(new FlightSchedule
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
                Console.WriteLine("Exception1:" + ex.Message);
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
                Console.WriteLine("Exception2:" + ex.Message);
            }
        }
    }
}
