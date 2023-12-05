using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.Airlines
{
    public class IndexModel : PageModel
    {
        public List<airlineInfo> listAirlines = new List<airlineInfo>();
        public void OnGet()
        {
            listAirlines.Clear();

            try
            {
                String connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Airlines";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                airlineInfo airLInfo = new airlineInfo();

                                airLInfo.airlineName = "" + reader.GetString(1);
                                airLInfo.airlineCode = "" + reader.GetString(2);



                                listAirlines.Add(airLInfo);
                            }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex);
            }
        }
    }
}
