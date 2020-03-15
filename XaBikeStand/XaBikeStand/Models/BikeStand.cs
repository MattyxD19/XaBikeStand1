using System;
using System.Collections.Generic;
using System.Text;

namespace XaBikeStand.Models
{
    class BikeStand
    {
        public int ID { get; set; }
        public bool IsWorking { get; set; }
        public string BikeStationID { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public bool InUse { get; set; }
    }
}
