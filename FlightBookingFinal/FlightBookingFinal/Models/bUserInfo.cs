namespace FlightBookingFinal.Models
{
    public class bUserInfo
    {
        public int id {  get; set; }
        public string username { get; set; }
        public string pasword { get; set; }
        public string email { get; set; }
        public string role { get; set; }

        public bUserInfo()
        {
        }

        public bUserInfo(int id, string username, string pasword, string email, string role)
        {
            this.id = id;
            this.username = username;
            this.pasword = pasword;
            this.email = email;
            this.role = role;
        }
    }
}
