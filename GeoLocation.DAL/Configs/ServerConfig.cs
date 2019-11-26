using System;
using System.Collections.Generic;
using System.Text;

namespace GeoLocation.DAL.Configs
{
    public class ServerConfig
    {
        public MongoDbConfig MongoDB { get; set; } = new MongoDbConfig();
    }
}
