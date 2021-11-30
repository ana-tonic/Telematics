using System;

namespace Server.Models
{
    public class Vehicle_location
    {
        public int Truck_Id { get; set; }
        public int Delivery_Id { get; set; }
        public string Location { get; set; }
        public DateTimeOffset Reading_time { get; set; }
        public int Distance;
        public string Unit;
    }
}