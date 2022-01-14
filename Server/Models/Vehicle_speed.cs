using System;
using Cassandra;

namespace Server.Models
{
    public class Vehicle_speed
    {
        public Cassandra.TimeUuid Delivery_Id { get; set; }
        public int Speed { get; set; }
        public DateTimeOffset Reading_time { get; set; }
        public string Unit { get; set; }
    }
}