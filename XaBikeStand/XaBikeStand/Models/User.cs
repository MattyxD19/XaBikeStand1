namespace XaBikeStand.Models
{
    public class User : ISerializable
    {
        public string userName { get; set; }
        public string psw { get; set; }
        public string email { get; set; }

        public string token { get; set; }
    }
}
