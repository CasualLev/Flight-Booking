namespace FlightBookingFinal.Models
{
    public class FlightSchedule
    {
        public string? RouteNumber { get; set; }
        public string? FlightName { get; set; }
        public string? DepartureAirport { get; set; }
        public string? DestinationAirport { get; set; }
        public int? EconomySeats { get; set; }
        public decimal? EconomyPrice { get; set; }
        public int? BusinessSeats { get; set; }
        public decimal? BusinessPrice { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }

        public FlightSchedule()
        {
        }

        public FlightSchedule(string? routeNumber, string? flightName, string? departureAirport, string? destinationAirport, int? economySeats, decimal? economyPrice, int? businessSeats, decimal? businessPrice, DateTime? departureTime, DateTime? arrivalTime)
        {
            RouteNumber = routeNumber;
            FlightName = flightName;
            DepartureAirport = departureAirport;
            DestinationAirport = destinationAirport;
            EconomySeats = economySeats;
            EconomyPrice = economyPrice;
            BusinessSeats = businessSeats;
            BusinessPrice = businessPrice;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }
    }

}
