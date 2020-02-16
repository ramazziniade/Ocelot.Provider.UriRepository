using System;
using System.Collections.Generic;
using System.Text;

namespace Ocelot.Provider.UriDiscovery.Client
{
    public class UriDiscoveryHost
    {
        public UriDiscoveryHost(string serviceId,string host, int port)
        {
            ServiceId = serviceId;
            Host = host;
            Port = port;
        }

        public string ServiceId { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }
}
