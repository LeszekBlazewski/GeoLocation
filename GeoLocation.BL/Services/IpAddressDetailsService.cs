using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GeoLocation.BL.RepositoryInterfaces;
using Models;

namespace GeoLocation.BL.Services.ServicesInterfaces
{
    public class IpAddressDetailsService : IIpAddressDetailsService
    {
        private readonly IIpAddressDetailsRepository _ipAddressDetailsRepository;

        public IpAddressDetailsService(IIpAddressDetailsRepository ipAddressDetailsRepository)
        {
            _ipAddressDetailsRepository = ipAddressDetailsRepository;
        }

        public async Task AddIpAddress(IpAddressDetails ipAddressDetail)
        {
            ipAddressDetail.Id = await _ipAddressDetailsRepository.GetNextAvailableId();

            await _ipAddressDetailsRepository.Add(ipAddressDetail);
        }

        public async Task<bool> DeleteIpAddress(long id)
        {
            return await _ipAddressDetailsRepository.Delete(id);
        }

        public async Task<IEnumerable<IpAddressDetails>> GetAllIpAddresses()
        {
            return await _ipAddressDetailsRepository.GetAllIpAddresses();
        }

        public async Task<IpAddressDetails> GetIpAddressDetail(long id)
        {
            return await _ipAddressDetailsRepository.GetIpAddressDetail(id);
        }

        public async Task<bool> UpdateIpAddress(IpAddressDetails ipAddressDetail)
        {
            return await _ipAddressDetailsRepository.Update(ipAddressDetail);
        }
    }
}
