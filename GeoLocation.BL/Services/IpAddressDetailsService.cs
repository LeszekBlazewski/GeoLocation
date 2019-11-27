using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeoLocation.BL.Properties;
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

        public async Task AddGeoLocationData(IpAddressDetails ipAddressDetail)
        {
            ipAddressDetail.Id = await _ipAddressDetailsRepository.GetNextAvailableId();

            await _ipAddressDetailsRepository.Add(ipAddressDetail);
        }

        public async Task<bool> DeleteGeoLocationData(long id)
        {
            var ipAddressFromDatabase = await _ipAddressDetailsRepository.GetById(id);

            if (ipAddressFromDatabase == null)
                throw new InvalidOperationException(Resources.DeleteGeoLocationAddressExcpetion);

            return await _ipAddressDetailsRepository.Delete(id);
        }

        public async Task<IEnumerable<IpAddressDetails>> GetAllGeoLocationData() => await _ipAddressDetailsRepository.GetAll();

        public async Task<IpAddressDetails> GetGeoLocationDataById(long id) => await _ipAddressDetailsRepository.GetById(id);

        public async Task<bool> UpdateGeoLocationData(IpAddressDetails ipAddressDetail)
        {
            var ipAddressFromDatabase = await _ipAddressDetailsRepository.GetById(ipAddressDetail.Id);

            if (ipAddressFromDatabase == null)
                throw new InvalidOperationException(Resources.UpdateGeoLocationAddressException);

            ipAddressDetail.DatabaseId = ipAddressFromDatabase.DatabaseId;

            return await _ipAddressDetailsRepository.Update(ipAddressDetail);
        }
    }
}
