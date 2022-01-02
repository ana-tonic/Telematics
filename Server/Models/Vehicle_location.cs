using Cassandra;
using System;

namespace Server.Models
{
    public class Vehicle_location
    {
        public TimeUuid Delivery_Id { get; set; }
        public location Location { get; set; }
        public DateTimeOffset Reading_time { get; set; }
        public int Distance { get; set; }
    }
}