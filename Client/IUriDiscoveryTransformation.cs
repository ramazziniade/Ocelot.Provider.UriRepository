using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ocelot.Provider.UriDiscovery.Client
{
    public interface IUriDiscoveryTransformation
    {
        Task<Uri[]> Transform(HttpResponseMessage response);
    }
    
    
}
