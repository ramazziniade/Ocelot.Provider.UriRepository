using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.UriDiscovery;
using Ocelot.Provider.UriDiscovery.Client;
using Ocelot.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ocelot.DependencyInjection
{
    public static class OcelotBuilderExtensions
    {
        public static IOcelotBuilder AddUriDiscovery<Transformation>(this IOcelotBuilder builder)
            where Transformation: IUriDiscoveryTransformation, new()
        {
            var service = builder.Services.First(x => x.ServiceType == typeof(IConfiguration));
            var configuration = (IConfiguration)service.ImplementationInstance;
            builder.Services.AddUriDiscoveryClient(configuration, new Transformation());
            builder.Services.AddSingleton<ServiceDiscoveryFinderDelegate>(UriDiscoveryProviderFactory.Get);
            builder.Services.AddSingleton<OcelotMiddlewareConfigurationDelegate>(UriDiscoveryMiddlewareConfigurationProvider.Get);
            return builder;
        }
    }
}
