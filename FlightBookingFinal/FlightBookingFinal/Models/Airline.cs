namespace FlightBookingFinal.Models
{
    public class airlineInfo
    {
        public int airlineID { get; set; }
        public string airlineName { get; set; }
        public string airlineCode { get; set; }

        public airlineInfo()
        {
        }

        public airlineInfo(int airlineID, string airlineName, string airlineCode)
        {
            this.airlineID = airlineID;
            this.airlineName = airlineName;
            this.airlineCode = airlineCode;
        }
    }
}
