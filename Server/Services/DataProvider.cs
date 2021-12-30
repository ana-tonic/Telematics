using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server;
using Server.Interfaces;
using Cassandra;
using Server.Models;

namespace Server.Services
{
    public class DataProvider
    {
        private  ISession Session { get; set; }

        public DataProvider()
        {
            Session = new AstraService().Session;
        }

        #region Deliveries
        public string getAllDeliveries()
        {
            return Session.Execute("select * from telematics.movies_and_tv").First().GetValue<string>("title");
        }

        public void CreateDelivery(Deliveries d)
        {
            Deliveries delivery = new Deliveries
            {
                Active = d.Active,
                Arrival_Time = d.Arrival_Time,
                Cargo = d.Cargo,
                Delivery_Id = Cassandra.TimeUuid.NewId(),
                Departing_Time = d.Departing_Time,
                Driver = d.Driver,
                End_Address = d.End_Address,
                Start_Address = d.Start_Address,
                Truck_Id = d.Truck_Id,
                Year = d.Year,
            };

            RowSet del = Session.Execute("insert into telematics.Deliveries (cargo, year, active, departing_time, delivery_id, arrival_time, driver, end_address, start_address, truck_id)"
                + $" values ('{delivery.Cargo}', {delivery.Year}, {delivery.Active}, '{delivery.Departing_Time.ToUnixTimeMilliseconds()}', {delivery.Delivery_Id}, '{delivery.Arrival_Time.ToUnixTimeMilliseconds()}', '{delivery.Driver}', " +
                $"'{delivery.End_Address}', '{delivery.Start_Address}', {delivery.Truck_Id})");

        }

        #endregion
    }
}