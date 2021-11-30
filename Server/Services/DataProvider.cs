using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server;
using Server.Interfaces;
using Cassandra;

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

        #endregion
    }
}