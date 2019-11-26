using Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GeoLocation.BL.Services.ServicesInterfaces
{
    public interface IIpStackService
    {
        public IpAddressDetails GetIpAddressDetails(string address, [Optional] string fields, [Optional] bool includeHostname, [Optional] bool includeSecurity, [Optional] string language);

        public IpAddressDetails GetIpAddressDetails(IEnumerable<string> ipAddresses);
    }
}
