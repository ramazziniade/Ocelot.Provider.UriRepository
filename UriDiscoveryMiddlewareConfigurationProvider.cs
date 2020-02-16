using Ocelot.Configuration;
using Ocelot.Configuration.Repository;
using Ocelot.Middleware;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ocelot.Provider.UriDiscovery
{
    internal static class UriDiscoveryMiddlewareConfigurationProvider
    {
        internal static OcelotMiddlewareConfigurationDelegate Get = builder =>
        {
            var internalConfigRepo = builder.ApplicationServices.GetService<IInternalConfigurationRepository>();

            var config = internalConfigRepo.Get();

            if (UsingUriDiscoveryServiceDiscoveryProvider(config.Data))
            {
                //builder.UseDiscoveryClient();
            }

            return Task.CompletedTask;
        };

        private static bool UsingUriDiscoveryServiceDiscoveryProvider(IInternalConfiguration configuration)
        {
            return configuration?.ServiceProviderConfiguration != null && configuration.ServiceProviderConfiguration.Type?.ToLower() == "uridiscovery";
        }
    }
}