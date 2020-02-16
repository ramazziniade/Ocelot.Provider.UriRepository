using Ocelot.Configuration;
using Ocelot.Provider.UriDiscovery.Client;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocelot.Provider.UriDiscovery
{
    public class UriDiscovery : IServiceDiscoveryProvider
    {
        private readonly IUriDiscoveryClient _client;
        private readonly ServiceProviderConfiguration _config;
        private readonly string _name;

        public UriDiscovery(
            IUriDiscoveryClient client,
            ServiceProviderConfiguration config,
            string name)
        {
            _client = client;
            _config = config;
            _name = name;
        }

        public async Task<List<Service>> Get()
        {
            var hosts = await _client.GetHosts(_name);
            return hosts.Select(item => new Service(_name,
                new ServiceHostAndPort(item.Host, item.Port),
                item.ServiceId, "", new string[0])).ToList();                      
        }
    }
}
