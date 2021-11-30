using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server;
using Server.Interfaces;

namespace Server.Services
{
    public class DataProvider
    {
        private static IDataStaxService Service { get; set; }



        #region Deliveries
        public static string getAllDeliveries()
        {
            return Service.Session.Execute("select * from telematics.movies_and_tv").First().GetValue<string>("title");
        }

        #endregion
    }
}