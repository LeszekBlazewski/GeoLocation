using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoLocation.BL.Services.ServicesInterfaces
{
    public interface IIpAddressDetailsService
    {
        Task<IEnumerable<IpAddressDetails>> GetAllGeoLocationData();
        Task<IpAddressDetails> GetGeoLocationDataById(long id);
        Task AddGeoLocationData(IpAddressDetails ipAddressDetail);
        Task<bool> UpdateGeoLocationData(IpAddressDetails ipAddressDetail);
        Task<bool> DeleteGeoLocationData(long id);
    }
}
