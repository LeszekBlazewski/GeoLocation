using GeoLocation.BL.Services.ServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Models;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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

        public IpAddressDetails GetIpAddressDetails(string ipAddress)
        {
            var request = new RestRequest();
            request.AddParameter("IpAddress", ipAddress, ParameterType.UrlSegment);
            request.Resource = "{IpAddress}";

            return Execute<IpAddressDetails>(request);
        }

        public IpAddressDetails GetIpAddressDetails(List<string> ipAddresses)
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
                const string message = "Error retrieving response. Check inner details for more info.";
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
