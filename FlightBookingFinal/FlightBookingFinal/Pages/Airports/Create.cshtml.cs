using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.Airports
{
    public class CreateModel : PageModel
    {
        public airportInfo airPInfo = new airportInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {

            airPInfo.airportCode = Request.Form["Code"];
            airPInfo.airportName = Request.Form["Name"];
            airPInfo.airportCity = Request.Form["City"];
            airPInfo.airportCountry = Request.Form["Country"];

            if (airPInfo.airportCode.Length == 0 || airPInfo.airportName.Length == 0 ||
                airPInfo.airportCity.Length == 0 || airPInfo.airportCountry.Length == 0)
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
                    String sqlQuery = "INSERT INTO Airports (Code, Name, City, Country) VALUES (@Code, @Name, @City, @Country)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@Code", airPInfo.airportCode);
                        cmd.Parameters.AddWithValue("@Name", airPInfo.airportName);
                        cmd.Parameters.AddWithValue("@City", airPInfo.airportCity);
                        cmd.Parameters.AddWithValue("@Country", airPInfo.airportCountry);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            airPInfo.airportCode = ""; airPInfo.airportName = "";
            airPInfo.airportCity = ""; airPInfo.airportCountry = "";

            successMessage = "New User Added";
            Response.Redirect("/Airports/Index");
        }
    }
}
