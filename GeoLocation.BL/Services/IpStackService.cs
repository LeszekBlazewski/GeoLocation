using GeoLocation.BL.Extenstions;
using GeoLocation.BL.Services.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Models;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;

namespace GeoLocation.BL.Services
{
    public class IpStackService : IIpStackService
    {
        private readonly string _baseUri;
        private readonly string _apiAccessKey;

        public IpStackService(IConfiguration configuration)
        {
            _baseUri = configuration.GetSection("IpStackBaseUri").Value;
            _apiAccessKey = configuration.GetSection("IpStackAccessKey").Value;
        }

        public IpAddressDetails GetIpAddressDetails(string address, [Optional] string fields, [Optional] bool includeHostname, [Optional] bool includeSecurity, [Optional] string language)
        {
            // check if valid ip address has been provided
            if (!IPAddress.TryParse(address, out _))
            {
                // if not try to get ip address from Uri
                address = Dns.GetHostAddresses(new Uri(address).Host)[0].ToString();
            }

            var request = new RestRequest();
            request.AddParameter("IpAddress", address, ParameterType.UrlSegment);
            request.Resource = "{IpAddress}";

            // Add optional parameters
            if (fields != null)
            {
                request.AddParameter("fields", fields);
            }
            if (includeHostname)
            {
                request.AddParameter("hostname", includeHostname.ConvertToInt());
            }
            if (includeSecurity)
            {
                request.AddParameter("security", includeSecurity.ConvertToInt());
            }
            if (!string.IsNullOrEmpty(language))
            {
                request.AddParameter("language", language);
            }

            return Execute<IpAddressDetails>(request);
        }

        public IpAddressDetails GetIpAddressDetails(IEnumerable<string> ipAddresses)
        {
            var request = new RestRequest();
            request.AddParameter("IpAddress", string.Join(",", ipAddresses), ParameterType.UrlSegment);
            request.Resource = "{IpAddress}";

            return Execute<IpAddressDetails>(request);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(_baseUri)
            };

            request.AddParameter("access_key", _apiAccessKey);

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response from Ipstack API.";
                throw new ApplicationException(message, response.ErrorException);
            }

            // Check for error object
            IDeserializer deserializer = new JsonDeserializer();
            ErrorResponse error = deserializer.Deserialize<ErrorResponse>(response);

            if (error.Success == false)
            {
                throw new ApplicationException(error.Error.Info);
            }

            return response.Data;
        }
    }
}
