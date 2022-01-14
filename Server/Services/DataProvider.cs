using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server;
using Server.Interfaces;
using Cassandra;
using Server.Models;
using System.Threading;

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

        public RowSet getDeliveries(string cargo, int year)
        {
            return Session.Execute($"SELECT * FROM telematics.deliveries WHERE cargo = '{cargo}' and year = {year}");
        }

        public void DeleteDelivery(string cargo, int year, Cassandra.TimeUuid delivery_id)
        {
            //Session.Execute($"delete from telematics.deliveries WHERE cargo = '{cargo}' and year = {year} and delivery_id = {delivery_id} if exists");
            Session.Execute($"delete from telematics.fuel using timestamp {DateTimeOffset.Now.ToUnixTimeMilliseconds()}  where  delivery_id = {delivery_id} and reading_time = '2022-01-03 12:24:31.994000+0000'  if exists");
            Session.Execute($"delete from telematics.location where  delivery_id = {delivery_id}  if exists");
            Session.Execute($"delete from telematics.idling where  delivery_id = {delivery_id}  if exists");
            Session.Execute($"delete from telematics.speed where  delivery_id = {delivery_id}  if exists");
        }

        public void CreateDelivery(Deliveries d)
        {
            Deliveries delivery = new Deliveries
            {
                Active = d.Active,
                Cargo = d.Cargo,
                Delivery_Id = d.Delivery_Id,
                Departing_Time = d.Departing_Time,
                Driver = d.Driver,
                End_Address = d.End_Address,
                Start_Address = d.Start_Address,
                Truck_Id = d.Truck_Id,
                Year = d.Year,
                Arrival_Time = d.Arrival_Time,
            };

            if (delivery.Arrival_Time < delivery.Departing_Time)

                Session.Execute("insert into telematics.Deliveries (cargo, year, active, departing_time, delivery_id, driver, end_address, start_address, truck_id)"
                    + $" values ('{delivery.Cargo}', {delivery.Year}, {delivery.Active}, '{delivery.Departing_Time.ToUnixTimeMilliseconds()}', {delivery.Delivery_Id}, '{delivery.Driver}', " +
                    $"'{delivery.End_Address}', '{delivery.Start_Address}', {delivery.Truck_Id})");
            else
                Session.Execute("insert into telematics.Deliveries (cargo, year, active, departing_time, delivery_id, driver, end_address, start_address, truck_id, arrival_time)"
                    + $" values ('{delivery.Cargo}', {delivery.Year}, {delivery.Active}, '{delivery.Departing_Time.ToUnixTimeMilliseconds()}', {delivery.Delivery_Id}, '{delivery.Driver}', " +
                    $"'{delivery.End_Address}', '{delivery.Start_Address}', {delivery.Truck_Id}, {delivery.Arrival_Time.ToUnixTimeMilliseconds()})");
        }

        public void StartDelivery(Deliveries d)
        {
            Random random = new Random();
            int duration = (random.Next() % 10000) + 60000;

            //generisanje fuel
            Vehicle_fuel vf = new Vehicle_fuel()
            {
                Delivery_Id = d.Delivery_Id,
                Fuel = (random.Next()%1000) + 500,
                Reading_Time = DateTimeOffset.Now,
                Unit = "l",
            };

            Vehicle_speed sp = new Vehicle_speed()
            {
                Delivery_Id = d.Delivery_Id,
                Reading_time = DateTimeOffset.Now,
                Speed = 0,
                Unit = "km/h",
            };

            Vehicle_idling_time idle = new Vehicle_idling_time()
            {
                Delivery_Id = d.Delivery_Id,
                Reading_Time = DateTimeOffset.Now,
                Time_Idle = 0,
                Unit = "min",
            };

            Vehicle_location loc = new Vehicle_location()
            {
                Delivery_Id = d.Delivery_Id,
                Reading_time = DateTimeOffset.Now,
                Distance = 0,
                Location = new location
                {
                    latitude = (random.Next() % 90) - 45,
                    longitude = (random.Next()  % 90) + 45,
                }
            };

            generateFuel(vf, duration);
            generateSpeed(sp, duration);
            generateIdle(idle, duration);
            generateLocation(loc, duration);

            Task.Delay(duration + 1000).ContinueWith(a => StopDelivery(d));
        }

        private void StopDelivery(Deliveries obj)
        {
            obj.Arrival_Time = DateTimeOffset.Now;
            obj.Active = false;
            CreateDelivery(obj); //modifikacija postojece stavke u bazi
        }

        #endregion

        #region fuel

        private void generateFuel(Vehicle_fuel vf, int remaining)
        {
            if (remaining < 0)
                return;
            remaining -= 3000;
            vf.Fuel -= 220;
            CreateFuel(vf);
            if (vf.Fuel < 300)
                vf.Fuel += 1000;
            Task.Delay(3000).ContinueWith(a => generateFuel(vf, remaining));
        }

        public RowSet getFuel(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.fuel WHERE delivery_id = {delivery_id} ");
        }


        public void CreateFuel(Vehicle_fuel fuel)
        {
            Session.Execute("insert into telematics.fuel (delivery_id, Reading_Time, fuel, Unit)"
                + $" values ({fuel.Delivery_Id}, '{DateTimeOffset.Now.ToUnixTimeMilliseconds()}', {fuel.Fuel}, '{fuel.Unit}')");
        }
        #endregion

        #region location

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
        private void generateLocation(Vehicle_location loc, int remaining)
        {
            Random random = new Random();
            if (remaining < 0)
                return;
            remaining -= 3000;
            loc.Distance += random.Next() % 5;
            CreateLocation(loc);
            loc.Location.latitude += (random.Next() % 6) - 3;
            loc.Location.longitude += (random.Next() % 6) - 3;
            Task.Delay(3000).ContinueWith(a => generateLocation(loc, remaining));
        }



        public void CreateLocation(Vehicle_location loc)
        {
            Session.Execute("insert into telematics.location (delivery_id, Reading_Time, distance, location)"
                + $" values ({loc.Delivery_Id}, '{DateTimeOffset.Now.ToUnixTimeMilliseconds()}', {loc.Distance}, {{longitude : {loc.Location.longitude}, latitude : {loc.Location.latitude}}})");
        }
        #endregion

        #region speed

        public RowSet getSpeed(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.speed WHERE delivery_id = {delivery_id} ");
        }

        public void CreateSpeed(Vehicle_speed speed)
        {
            Session.Execute("insert into telematics.speed (delivery_id, Reading_Time, speed, Unit)"
                + $" values ({speed.Delivery_Id}, '{DateTimeOffset.Now.ToUnixTimeMilliseconds()}', {speed.Speed}, '{speed.Unit}')");
        }
        private void generateSpeed(Vehicle_speed sp, int remaining)
        {
            Random random = new Random();
            if (remaining < 0)
                return;
            remaining -= 3000;
            sp.Speed += random.Next() % 20 - 5;
            if (sp.Speed < 0)
                sp.Speed = 0;
            if (sp.Speed > 120)
                sp.Speed = 120;
            CreateSpeed(sp);
            Task.Delay(3000).ContinueWith(a => generateSpeed(sp, remaining));
        }
        #endregion

        #region idle
        public RowSet getIdling(Cassandra.TimeUuid delivery_id)
        {
            return Session.Execute($"SELECT * FROM telematics.idling WHERE delivery_id = {delivery_id} ");
        }

        public void CreateIdling(Vehicle_idling_time idle)
        {
            Session.Execute("insert into telematics.idling (delivery_id, Reading_Time, time_idle, Unit)"
                + $" values ({idle.Delivery_Id}, '{DateTimeOffset.Now.ToUnixTimeMilliseconds()}', {idle.Time_Idle}, '{idle.Unit}')");
        }

        private void generateIdle(Vehicle_idling_time idle, int remaining)
        {
            Random random = new Random();
            if (remaining < 0)
                return;
            remaining -= 3000;
            if ((random.Next() % 5) == 0)
                idle.Time_Idle += 1;

            CreateIdling(idle);
            Task.Delay(3000).ContinueWith(a => generateIdle(idle, remaining));
        }

        #endregion
    }
}