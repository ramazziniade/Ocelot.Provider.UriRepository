using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ocelot.Provider.UriDiscovery.Client
{
    public interface IUriDiscoveryClient
    {
        Task<UriDiscoveryHost[]> GetHosts(string serviceName);
    }
    public class UriDiscoveryClient: IUriDiscoveryClient
    {
        private readonly ILogger<UriDiscoveryClient> _logger;
        private readonly HttpClient _client;
        private readonly UriDiscoveryConfiguration _options;
        private readonly IUriDiscoveryTransformation _transformation;

        public UriDiscoveryClient(
            ILogger<UriDiscoveryClient> logger, 
            HttpClient client,
            IOptions<UriDiscoveryConfiguration> options,
            IUriDiscoveryTransformation transformation)
        {
            _logger = logger;
            _client = client;
            _options = options.Value;
            _transformation = transformation;
        }

        public async Task<UriDiscoveryHost[]> GetHosts(string serviceName)
        {
            try
            {
                string uri = string.IsNullOrEmpty(_options.Uri) ? serviceName: $"{_options.Uri}/{serviceName}";
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var uriArray = await _transformation.Transform(response);
                    return CreateDiscoveryHost(serviceName, uriArray);
                }
                    

                _logger.LogTrace($"Get Hosts - StatusCode: {response.StatusCode} - Content: {await response.Content.ReadAsStringAsync() }");
                return new UriDiscoveryHost[0];

            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, "Error: Discovery Client");
                return new UriDiscoveryHost[0];                 
            }
        }

        private UriDiscoveryHost[] CreateDiscoveryHost(string serviceName, Uri[] uriArray)
        {
            return uriArray
                .Select(item => new UriDiscoveryHost($"{serviceName}-{item.Host}-{item.Port}", item.Host, item.Port))
                .ToArray();            
        }
    }
}
