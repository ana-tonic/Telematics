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
        private ISession Session { get; set; }

        public DataProvider()
        {
            Session = new AstraService().Session;
        }

        #region Deliveries
        public string getAllDeliveries()
        {
            return Session.Execute("select * from telematics.movies_and_tv").First().GetValue<string>("title");
        }

        public RowSet getDeliveries(string cargo, int year)
        {
            return Session.Execute($"SELECT * FROM telematics.deliveries WHERE cargo = '{cargo}' and year = {year}");
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

            Session.Execute("insert into telematics.Deliveries (cargo, year, active, departing_time, delivery_id, arrival_time, driver, end_address, start_address, truck_id)"
                + $" values ('{delivery.Cargo}', {delivery.Year}, {delivery.Active}, '{delivery.Departing_Time.ToUnixTimeMilliseconds()}', {delivery.Delivery_Id}, '{delivery.Arrival_Time.ToUnixTimeMilliseconds()}', '{delivery.Driver}', " +
                $"'{delivery.End_Address}', '{delivery.Start_Address}', {delivery.Truck_Id})");

        }

        public RowSet getFuel(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.fuel WHERE delivery_id = {delivery_id} ");
        }

        public void CreateFuel(Cassandra.TimeUuid delivery_id)
        {
            Session.Execute("insert into telematics.fuel (delivery_id, Reading_Time, fuel, truck_id, Unit)"
                + $" values ({delivery_id}, '2021-12-31T11:06:01.513Z', 600, 100, 'l')");
        }

        public RowSet getLocation(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.location WHERE delivery_id = {delivery_id} ");
        }

        public RowSet getSpeed(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.speed WHERE delivery_id = {delivery_id} ");
        }

        public RowSet getIdling(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.idling WHERE delivery_id = {delivery_id} ");
        }

        #endregion
    }
}