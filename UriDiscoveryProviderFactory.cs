using System;
using Ocelot.Provider.UriDiscovery.Client;
using Ocelot.ServiceDiscovery;
using Microsoft.Extensions.DependencyInjection;

namespace Ocelot.Provider.UriDiscovery
{
    internal static class UriDiscoveryProviderFactory
    {
        internal static ServiceDiscoveryFinderDelegate Get = (provide, config, name) =>
        {
            if (config.Type?.ToLower() == "uridiscovery")
            {
                return new UriDiscovery(provide.GetService<IUriDiscoveryClient>(), config, name);
            }

            return null;
        };
    }
}