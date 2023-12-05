using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.BookingUsers
{
    public class IndexModel : PageModel
    {
        public List<bUserInfo> listUsers = new List<bUserInfo>();

        public void OnGet()
        {
            listUsers.Clear();
            try
            {
                String connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Users";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                bUserInfo BUInfo = new bUserInfo();
                                BUInfo.id = reader.GetInt32(0);
                                BUInfo.username = "" + reader.GetString(1);
                                BUInfo.pasword = "" + reader.GetString(2);
                                BUInfo.email = "" + reader.GetString(3);
                                BUInfo.role = "" + reader.GetString(4);

                                listUsers.Add(BUInfo);
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
