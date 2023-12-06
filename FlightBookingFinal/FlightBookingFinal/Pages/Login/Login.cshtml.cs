using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.Login
{
    public class LoginModel : PageModel
    {
        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "All fields must be filled";
                return Page();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        string role = (string)cmd.ExecuteScalar();

                        if (role != null)
                        {
                            // Successful login
                            // Redirect to the appropriate page based on the role
                            if (role == "Admin")
                            {
                                return RedirectToPage("/AdminDashboard/adminDash");
                            }
                            else if (role == "Customer")
                            {
                                return RedirectToPage("/CustomerBooking/Search");
                            }
                        }
                        else
                        {
                            ErrorMessage = "Invalid username or password. Please try again.";
                            return Page();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while processing your request. Please try again.";
                // Log the detailed error for your reference
                Console.WriteLine(ex.ToString());
                return Page();
            }
            return Page();
        }
    }
}
