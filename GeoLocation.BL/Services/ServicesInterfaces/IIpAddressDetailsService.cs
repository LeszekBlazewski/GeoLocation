using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocation.BL.Services.ServicesInterfaces
{
    public interface IIpAddressDetailsService
    {
        Task<IEnumerable<IpAddressDetails>> GetAllIpAddresses();
        Task<IpAddressDetails> GetIpAddressDetail(long id);
        Task AddIpAddress(IpAddressDetails ipAddressDetail);
        Task<bool> UpdateIpAddress(IpAddressDetails ipAddressDetail);
        Task<bool> DeleteIpAddress(long id);
    }
}
