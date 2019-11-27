using GeoLocation.BL.RepositoryInterfaces;
using GeoLocation.DAL.Properties;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async Task Add(IpAddressDetails ipAddressDetail)
        {
            try
            {
                await _context.GeoLocations.InsertOneAsync(ipAddressDetail);
            }
            catch (Exception ex) when (ex is MongoException || ex is TimeoutException)
            {
                throw new ApplicationException(Resources.DatabaseUnavailableException, ex);
            }
        }

        public async Task<IEnumerable<IpAddressDetails>> GetAll()
        {
            try
            {
                return await _context.GeoLocations.AsQueryable().ToListAsync();
            }
            catch (Exception ex) when (ex is MongoException || ex is TimeoutException)
            {
                throw new ApplicationException(Resources.DatabaseUnavailableException, ex);
            }
        }

        public Task<IpAddressDetails> GetById(long id)
        {
            try
            {
                FilterDefinition<IpAddressDetails> filter = Builders<IpAddressDetails>.Filter.Eq(ip => ip.Id, id);
                return _context.GeoLocations.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex) when (ex is MongoException || ex is TimeoutException)
            {
                throw new ApplicationException(Resources.DatabaseUnavailableException, ex);
            }
        }

        public async Task<bool> Update(IpAddressDetails ipAddressDetail)
        {
            try
            {
                var updateResult = await _context.GeoLocations.ReplaceOneAsync(ip => ip.Id == ipAddressDetail.Id, ipAddressDetail);

                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex) when (ex is MongoException || ex is TimeoutException)
            {
                throw new ApplicationException(Resources.DatabaseUnavailableException, ex);
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var deleteResult = await _context.GeoLocations.DeleteOneAsync(Builders<IpAddressDetails>.Filter.Eq(ip => ip.Id, id));
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex) when (ex is MongoException || ex is TimeoutException)
            {
                throw new ApplicationException(Resources.DatabaseUnavailableException, ex);
            }
        }

        public async Task<long> GetNextAvailableId()
        {
            try
            {
                return await _context.GeoLocations.CountDocumentsAsync(new BsonDocument()) + 1;
            }
            catch (Exception ex) when (ex is MongoException || ex is TimeoutException)
            {
                throw new ApplicationException(Resources.DatabaseUnavailableException, ex);
            }
        }
    }
}
