using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLocation.DAL
{
    public interface IDatabaseContext
    {
        IMongoCollection<IpAddressDetails> GeoLocations { get; }
    }
}
