using Models;

namespace GeoLocation.API.Dtos
{
    public class IpAddressDetailsDto
    {
        public long Id { get; set; }

        public string Ip { get; set; }

        public string Hostname { get; set; }

        public string Type { get; set; }


        public string ContinentCode { get; set; }


        public string ContinentName { get; set; }

        public string CountryCode { get; set; }


        public string CountryName { get; set; }


        public string RegionCode { get; set; }


        public string RegionName { get; set; }


        public string City { get; set; }


        public string Zip { get; set; }


        public double Latitude { get; set; }


        public double Longitude { get; set; }


        public Location Location { get; set; }

        public Models.TimeZone TimeZone { get; set; }


        public Currency Currency { get; set; }


        public Connection Connection { get; set; }


        public Security Security { get; set; }
    }
}
