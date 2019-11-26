using Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GeoLocation.BL.Services.ServicesInterfaces
{
    public interface IIpStackService
    {
        public IpAddressDetails GetIpAddressDetails(string ipAddress);

        public IpAddressDetails GetIpAddressDetails(List<string> ipAddresses);
    }
}
