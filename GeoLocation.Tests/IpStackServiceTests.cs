using GeoLocation.BL.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;

namespace GeoLocation.Tests
{
    [TestFixture]
    public class IpStackServiceTests
    {
        static string[] Addresses =
        {
            "104.154.102.125",
            "200.213.91.230",
            "19.20.150.35",
            "https://www.forbes.com/",
            "https://twitter.com/",
            "https://azure.microsoft.com/en-us/"
        };


        private IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            var configMoq = new Mock<IConfiguration>();

            configMoq.SetupGet(x => x.GetSection("IpStackBaseUri").Value).Returns("http://api.ipstack.com/");
            configMoq.SetupGet(x => x.GetSection("IpStackAccessKey").Value).Returns("cd503a32bb9a2ba1d19cda512113ae22");

            _configuration = configMoq.Object;
        }

        [TestCaseSource("Addresses")]
        public void GetGeoLocationData_ValidAddress_AllFields_GeoLocationDataForALLNonPaidSubscriptionReturned(string address)
        {
            //Arrange
            var ipStackService = new IpStackService(_configuration);

            //Action
            var result = ipStackService.GetIpAddressDetails(address);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.City);
                Assert.IsNotEmpty(result.Ip);
                Assert.IsNotNull(result.Latitude);
                Assert.IsNotNull(result.Longitude);
            });
        }

        [TestCaseSource("Addresses")]
        public void GetGeoLocationData_ValidIpAddress_OnlySelectedFields_GeoLocationDataForSelectedFieldsReturned(string address)
        {
            //Arrange
            var ipStackService = new IpStackService(_configuration);

            //Action
            var result = ipStackService.GetIpAddressDetails(address, "ip,country_code,location.capital");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Ip);
                Assert.IsNotEmpty(result.CountryCode);
                Assert.IsNotEmpty(result.Location.Capital);
            });
        }

        [TestCaseSource("Addresses")]
        public void GetGeoLocationData_ValidIpAddress_SpecialFieldsSelected_GeoLocationDataForSelectedFieldsReturned(string address)
        {
            //Arrange
            var ipStackService = new IpStackService(_configuration);

            //Action
            var result = ipStackService.GetIpAddressDetails(address, includeHostname: true, includeSecurity: true, language: "en");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Hostname);
                Assert.IsNull(result.Security);     // THIS should be null because of free subscription (security is not available)
            });
        }

        [TestCase("asdhjasdjhkasdjh")]
        [TestCase("1.1.1.1.1.1.1.1.")]
        [TestCase("www.sadasdasdasdqwr123123xzc213.")]
        public void GetGeoLocationData_InvalidInputAddress_ExceptionIsThrown(string address)
        {
            //Arrange
            var ipStackService = new IpStackService(_configuration);

            //Assert
            Assert.Throws<UriFormatException>(() => ipStackService.GetIpAddressDetails(address));
        }
    }
}
