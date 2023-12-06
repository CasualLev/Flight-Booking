using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace FlightBookingFinal.Pages.BookingUsers
{
    public class CreateModel : PageModel
    {
        public bUserInfo BUserInfo = new bUserInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            BUserInfo.username = Request.Form["username"];
            BUserInfo.pasword = Request.Form["password"];
            BUserInfo.email = Request.Form["email"];
            BUserInfo.role = Request.Form["role"];

            if (BUserInfo.username.Length == 0 || BUserInfo.pasword.Length == 0 ||
                BUserInfo.email.Length == 0 || BUserInfo.role.Length == 0)
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
                    String sqlQuery = "INSERT INTO Users (Username,Password,Email,Role) VALUES(@username,@password,@email,@role)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        string hashedPassword = HashPassword(BUserInfo.pasword);
                        cmd.Parameters.AddWithValue("@username", BUserInfo.username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@email", BUserInfo.email);
                        cmd.Parameters.AddWithValue("@role", BUserInfo.role);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            BUserInfo.username = ""; BUserInfo.pasword = "";
            BUserInfo.email = ""; BUserInfo.role = "";

            successMessage = "New User Added";
            Response.Redirect("/BookingUsers/Index");
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
