namespace FlightBookingFinal.Models
{
    public class BookingInfo
    {
        public int BookingID { get; set; }

        public string FlightRouteNumber { get; set; }

        public string Class { get; set; }

        public int NumTickets { get; set; }

        public bool RoundTrip { get; set; }

        public decimal TotalPrice { get; set; }

        public BookingInfo()
        {
        }

        public BookingInfo(int bookingID, string flightRouteNumber, string @class, int numTickets, bool roundTrip, decimal totalPrice)
        {
            BookingID = bookingID;
            FlightRouteNumber = flightRouteNumber;
            Class = @class;
            NumTickets = numTickets;
            RoundTrip = roundTrip;
            TotalPrice = totalPrice;
        }
    }
}
