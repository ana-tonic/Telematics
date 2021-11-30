using System;
using Newtonsoft.Json;

namespace Server.Models
{
    public class Vehicle_idling_time
    {
        public int Truck_Id  { get; set; }
        
        public int Delivery_Id { get; set; }
        public DateTimeOffset Time_Idle { get; set; }
        public DateTimeOffset Reading_Time { get; set; }
        public string Unit { get; set; }
    }

}