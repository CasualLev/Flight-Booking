using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.Airports
{
    public class IndexModel : PageModel
    {
        public List<airportInfo> listAirports = new List<airportInfo>();
        public void OnGet()
        {
            listAirports.Clear();

            try
            {
                String connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Airports";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                airportInfo airPInfo = new airportInfo();

                                airPInfo.airportCode = "" + reader.GetString(1);
                                airPInfo.airportName = "" + reader.GetString(2);
                                airPInfo.airportCity = "" + reader.GetString(3);
                                airPInfo.airportCountry = "" + reader.GetString(4);

                                listAirports.Add(airPInfo);
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }
    }
}
