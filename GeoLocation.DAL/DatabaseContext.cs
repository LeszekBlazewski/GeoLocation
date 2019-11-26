using System;
using System.Collections.Generic;
using System.Text;
using GeoLocation.DAL.Configs;
using Models;
using MongoDB.Driver;

namespace GeoLocation.DAL
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(MongoDbConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _database = client.GetDatabase(config.Database);
        }

        public IMongoCollection<IpAddressDetails> GeoLocations => _database.GetCollection<IpAddressDetails>("IpAddressDetails");
    }
}
