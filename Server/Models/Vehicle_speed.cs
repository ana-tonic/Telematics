using System;

namespace Server.Models
{
    public class Vehicle_speed
    {
        public int Truck_Id { get; set; }
        public int Delivery_Id { get; set; }
        public int Speed { get; set; }
        public DateTimeOffset Reading_time { get; set; }
        public string Unit { get; set; }
    }
}