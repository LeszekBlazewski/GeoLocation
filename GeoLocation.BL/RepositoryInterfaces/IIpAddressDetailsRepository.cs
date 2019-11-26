using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocation.BL.RepositoryInterfaces
{
    public interface IIpAddressDetailsRepository
    {
        Task<IEnumerable<IpAddressDetails>> GetAllIpAddresses();
        Task<IpAddressDetails> GetIpAddressDetail(long id);
        Task Add(IpAddressDetails ipAddressDetail);
        Task<bool> Update(IpAddressDetails ipAddressDetail);
        Task<bool> Delete(long id);
        Task<long> GetNextAvailableId();
    }
}
