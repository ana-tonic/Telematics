using System;

namespace Server.Models
{
    public class Deliveries
    {
        public int Truck_Id { get; set; }
        public int Delivery_Id { get; set; }
        public string Driver { get; set; }
        public string Start_Address { get; set; }
        public string End_Address { get; set; }
        public DateTimeOffset Departing_Time { get; set; }
        public DateTimeOffset Arrival_Time { get; set; }
        public string Cargo { get; set; }
        public bool Active { get; set; }
    }

}