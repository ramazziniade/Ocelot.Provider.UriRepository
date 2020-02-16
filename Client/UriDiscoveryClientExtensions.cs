using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocelot.Provider.UriDiscovery.Client
{
    public static class UriDiscoveryClientExtensions
    {
        public static IServiceCollection AddUriDiscoveryClient(this IServiceCollection services, IConfiguration configuration, IUriDiscoveryTransformation transformation)
        {            
            services.Configure<UriDiscoveryConfiguration>(configuration.GetSection("UriDiscovery"));
            services.AddSingleton<IUriDiscoveryTransformation>(transformation);
            services.AddHttpClient<IUriDiscoveryClient, UriDiscoveryClient>((provider, client) =>
            {
                client.BaseAddress = new Uri(provider.GetService<IOptions<UriDiscoveryConfiguration>>().Value.ServiceUri);
            });

            return services;
        }

        public static IApplicationBuilder UseUriDiscoveryClient(this IApplicationBuilder builder)
        {
            return builder;
        }
    }
}
