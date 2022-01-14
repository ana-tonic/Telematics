using System;
using Cassandra;

namespace Server.Models
{
    public class Vehicle_fuel
    {
        public TimeUuid Delivery_Id { get; set; }
        public double Fuel { get; set; }
        public DateTimeOffset Reading_Time { get; set; }
        public string Unit { get; set; }
    }
}