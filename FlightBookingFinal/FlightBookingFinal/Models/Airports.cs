namespace FlightBookingFinal.Models
{
    public class airportInfo
    {
        public int airportID {  get; set; }
        public string airportCode { get; set; }
        public string airportName { get; set; }
        public string airportCity { get; set; }
        public string airportCountry { get; set; }

        public airportInfo()
        {
        }

        public airportInfo(int airportID, string airportCode, string airportName, string airportCity, string airportCountry)
        {
            this.airportID = airportID;
            this.airportCode = airportCode;
            this.airportName = airportName;
            this.airportCity = airportCity;
            this.airportCountry = airportCountry;
        }
    }

}
