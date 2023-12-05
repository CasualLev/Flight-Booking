using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.Airlines
{
    public class CreateModel : PageModel
    {
        public airlineInfo airLInfo = new airlineInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {

            airLInfo.airlineName = Request.Form["Name"];
            airLInfo.airlineCode = Request.Form["Code"];


            if (airLInfo.airlineName.Length == 0 || airLInfo.airlineCode.Length == 0)
            {
                errorMessage = "All fields must be filled";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "INSERT INTO Airlines (Name, IATACode) VALUES (@Name, @Code)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@Name", airLInfo.airlineName);
                        cmd.Parameters.AddWithValue("@Code", airLInfo.airlineCode);


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            airLInfo.airlineName = ""; airLInfo.airlineCode = "";


            successMessage = "New Airline Added";
            Response.Redirect("/Airlines/Index");
        }
    }
}
