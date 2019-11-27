using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLocation.BL.RepositoryInterfaces
{
    public interface IIpAddressDetailsRepository
    {
        Task<IEnumerable<IpAddressDetails>> GetAll();
        Task<IpAddressDetails> GetById(long id);
        Task Add(IpAddressDetails ipAddressDetail);
        Task<bool> Update(IpAddressDetails ipAddressDetail);
        Task<bool> Delete(long id);
        Task<long> GetNextAvailableId();
    }
}
