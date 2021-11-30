using System;

namespace Server.Models
{
    public class Vehicle_speed
    {
        public int Truck_Id;
        public int Delivery_Id;
        public int Speed;
        public DateTimeOffset Reading_time;
        public string Unit;
    }
}