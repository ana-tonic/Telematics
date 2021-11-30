
using System;
using Cassandra;
using System.Threading;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Services
{
    public class AstraService : Interfaces.IDataStaxService
    {
        private ISession _session;
        public ISession Session
        {
            get
            {
                if (_session == null)
                {
                    _session = Cluster.Builder()
                                            .WithCloudSecureConnectionBundle("secure-connect-test.zip")
                                            .WithCredentials("wvXCpORCjsTiGndYoaeeXApJ", "UmZSGivTw5kS91N4jNQzixCvR2vGwLKyQAyX9f8x1emB,f9BtEzlfZA-c5+1L,wzOJdMF6jgbEdInIdzg1zclLsst6hMDeeov7Zb_M,FZS8v+J.MOCmH4-g__9vZW_aj")
                                            .Build()
                                            .Connect();
                }
                return _session;
            }
        }
    }
}