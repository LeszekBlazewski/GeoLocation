using System.ComponentModel;

namespace GeoLocation.API.Queries
{
    public class IpAddressQuery
    {
        public string Address { get; set; }

        //optional parameter to specifiy fields which should be returned in response
        public string Fields { get; set; }

        //optional parameter to specifiy languge in which data should be returned
        public string Language { get; set; }

        public bool IncludeHostName { get; set; }

        public bool IncludeSecurity { get; set; }

    }
}
