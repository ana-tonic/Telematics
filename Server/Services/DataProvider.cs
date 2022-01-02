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
            Session.UserDefinedTypes.DefineAsync(
                UdtMap.For<location>(keyspace: "telematics")
                .Map(l => l.longitude, "longitude")
                .Map(l => l.latitude, "latitude")).ConfigureAwait(false);
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

        public void StartDelivery(Cassandra.TimeUuid delivery_id)
        {
            Task.Delay.
        }

        public RowSet getFuel(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.fuel WHERE delivery_id = {delivery_id} ");
        }

        public void CreateFuel(Cassandra.TimeUuid delivery_id)
        {
            Session.Execute("insert into telematics.fuel (delivery_id, Reading_Time, fuel, Unit)"
                + $" values ({delivery_id}, '{DateTimeOffset.Now.ToUnixTimeSeconds()}', 600, 'l')");
        }

        public List<Vehicle_location> getLocation(Cassandra.TimeUuid delivery_id)
        {
            RowSet rows = Session.Execute($"SELECT * FROM telematics.location WHERE delivery_id = {delivery_id} ");
            List<Vehicle_location> locations = new List<Vehicle_location>();
            foreach (Row row in rows)
            {
                Vehicle_location l = new Vehicle_location()
                {
                    Delivery_Id = row.GetValue<Guid>("delivery_id"),
                    Distance = row.GetValue<int>("distance"),
                    Location = row.GetValue<location>("location"),
                    Reading_time = row.GetValue<DateTimeOffset>("reading_time"),
                };
                locations.Add(l);
            }

            return locations;
        }

        public void CreateLocation(Cassandra.TimeUuid delivery_id)
        {
            Session.Execute("insert into telematics.location (delivery_id, Reading_Time, distance, location)"
                + $" values ({delivery_id}, '{DateTimeOffset.Now.ToUnixTimeSeconds()}', 600, {{longitude : 200.3, latitude : 200.5}})");
        }

        public RowSet getSpeed(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.speed WHERE delivery_id = {delivery_id} ");
        }

        public void CreateSpeed(Cassandra.TimeUuid delivery_id)
        {
            Session.Execute("insert into telematics.speed (delivery_id, Reading_Time, speed, Unit)"
                + $" values ({delivery_id}, '{DateTimeOffset.Now.ToUnixTimeSeconds()}', 120, 'km/h')");
        }

        public RowSet getIdling(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.idling WHERE delivery_id = {delivery_id} ");
        }

        public void CreateIdling(Cassandra.TimeUuid delivery_id)
        {
            Session.Execute("insert into telematics.idling (delivery_id, Reading_Time, time_idle, Unit)"
                + $" values ({delivery_id}, '{DateTimeOffset.Now.ToUnixTimeSeconds()}', 2, 'H')");
        }

        #endregion
    }
}