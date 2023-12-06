using FlightBookingFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace FlightBookingFinal.Pages.CustomerBooking
{
    public class BookingModel : PageModel
    {
        [BindProperty]
        public BookingInfo BInfo { get; set; }

        // Add a property to hold the list of Flight Route Numbers
        public List<SelectListItem> FlightRouteNumbers { get; set; } = new List<SelectListItem>();

        public IActionResult OnGet()
        {
            // Load any necessary data for the booking page
            LoadFlightRouteNumbers(); // Load Flight Route Numbers

            // For example, load user details, etc.
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                LoadFlightRouteNumbers(); // Reload Flight Route Numbers
                return Page();
            }

            // Save the booking information to the database
            SaveBookingToDatabase(BInfo);

            // Redirect to a confirmation page or any other desired page
            return RedirectToPage("/CustomerBooking/Confirmation");
        }

        private void SaveBookingToDatabase(BookingInfo booking)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "INSERT INTO Bookings (FlightRouteNumber, Class, NumTickets, RoundTrip, TotalPrice) " +
                                      "VALUES (@FlightRouteNumber, @Class, @NumTickets, @RoundTrip, @TotalPrice)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@FlightRouteNumber", booking.FlightRouteNumber);
                        cmd.Parameters.AddWithValue("@Class", booking.Class);
                        cmd.Parameters.AddWithValue("@NumTickets", booking.NumTickets);
                        cmd.Parameters.AddWithValue("@RoundTrip", booking.RoundTrip);
                        cmd.Parameters.AddWithValue("@TotalPrice", CalculateTotalPrice(booking));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle database exception
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private decimal CalculateTotalPrice(BookingInfo booking)
        {
            decimal basePrice = 100.0m; // Adjust the base price as needed

            // Additional pricing factors based on the selected class
            decimal classPriceFactor = booking.Class.ToLower() == "business" ? 1.5m : 1.0m;

            // Additional pricing factors based on the number of tickets
            decimal numTicketsPriceFactor = booking.NumTickets > 1 ? 0.9m : 1.0m;

            // Additional pricing factors based on round trip
            decimal roundTripPriceFactor = booking.RoundTrip ? 1.2m : 1.0m;

            // Calculate the total price
            decimal totalPrice = basePrice * classPriceFactor * numTicketsPriceFactor * roundTripPriceFactor;

            return totalPrice;
        }

        private void LoadFlightRouteNumbers()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-ST9MH65;Initial Catalog=FBookingDBV2;Integrated Security=True;Encrypt=False";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT FlightRouteNumber FROM FlightSchedules";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FlightRouteNumbers.Add(new SelectListItem
                                {
                                    Value = reader.GetString(0), // Use GetString for string data
                                    Text = reader.GetString(0)
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
    }
    
}
