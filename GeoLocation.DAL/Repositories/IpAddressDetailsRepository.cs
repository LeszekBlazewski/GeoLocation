using GeoLocation.BL.RepositoryInterfaces;
using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocation.DAL.Repositories
{
    public class IpAddressDetailsRepository : IIpAddressDetailsRepository
    {
        private readonly IDatabaseContext _context;

        public IpAddressDetailsRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Add(IpAddressDetails ipAddressDetail) => await _context.GeoLocations.InsertOneAsync(ipAddressDetail);

        public async Task<IEnumerable<IpAddressDetails>> GetAllIpAddresses() => await _context.GeoLocations.AsQueryable().ToListAsync();

        public Task<IpAddressDetails> GetIpAddressDetail(long id)
        {
            FilterDefinition<IpAddressDetails> filter = Builders<IpAddressDetails>.Filter.Eq(ip => ip.Id, id);
            return _context.GeoLocations.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(IpAddressDetails ipAddressDetail)
        {
            var updateResult = await _context.GeoLocations.ReplaceOneAsync(ip => ip.Id == ipAddressDetail.Id, ipAddressDetail);

            return updateResult.IsAcknowledged;
        }

        public async Task<bool> Delete(long id)
        {
            var deleteResult = await _context.GeoLocations.DeleteOneAsync(Builders<IpAddressDetails>.Filter.Eq(ip => ip.Id, id));
            return deleteResult.IsAcknowledged;
        }

        public async Task<long> GetNextAvailableId() => await _context.GeoLocations.CountDocumentsAsync(null) + 1;
    }
}
