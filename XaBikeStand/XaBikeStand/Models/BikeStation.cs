using System;

namespace XaBikeStand.Models
{
    public class BikeStation : ISerializable
    {
        public String bikeStationID { get; set; }
        public String title { get; set; }
        public double longtitude { get; set; }

        public double latitude { get; set; }


    }
}
