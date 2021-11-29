
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
                                            .WithCredentials("gISIwpTraUOfWMJTrPWDcoxJ", "SPHmw3TDSXoENZ,87krwuG_0QSvRoSqZacUl9uhwPtmK5+fYnwwW4nzBLM0QDGt77LZ1vhQJpH.Q8PfqM5+Y6i9dS1SUXZ,lfLQOqUCNKJuuRM6FhCPn.Ho5rB-zC9cB")
                                            .Build()
                                            .Connect();
                }
                return _session;
            }
        }
    }
}