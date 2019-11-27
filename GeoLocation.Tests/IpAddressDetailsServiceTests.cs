using GeoLocation.BL.RepositoryInterfaces;
using GeoLocation.BL.Services.ServicesInterfaces;
using Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLocation.Tests
{
    [TestFixture]
    public class IpAddressDetailsServiceTests
    {
        [Test]
        public async Task AddGeoLocationData_ValidObjectPassed_NewDataIsAdded()
        {
            //Arrange

            var mockAddressDetailsRepository = new Mock<IIpAddressDetailsRepository>();

            var addressDetailsService = new IpAddressDetailsService(mockAddressDetailsRepository.Object);

            var iIpAddressDetails = new IpAddressDetails()
            {
                City = "City",
                ContinentCode = "Code",
                ContinentName = "ConnectionName",
                CountryCode = "ContryCode",
                CountryName = "CountryName",
                DatabaseId = new MongoDB.Bson.ObjectId(),
                Hostname = "hostName",
                Id = 1,
                Ip = "127.0.0.1"
            };

            //Action

            await addressDetailsService.AddGeoLocationData(iIpAddressDetails);

            //Assert

            mockAddressDetailsRepository.Verify(m => m.Add(iIpAddressDetails), Times.Once);
        }

        [Test]
        public async Task UpdateGeoLocationData_ObjectExistsInDatabase_ObjectIsUpdated()
        {
            //Arrange

            var iIpAddressDetailsFromDb = new IpAddressDetails()
            {
                City = "City",
                ContinentCode = "Code",
                ContinentName = "ConnectionName",
                CountryCode = "ContryCode FROM DB",
                CountryName = "CountryName FROM DB",
                DatabaseId = new MongoDB.Bson.ObjectId(),
                Hostname = "FROM DB",
                Id = 1,
                Ip = "127.0.0.1"
            };

            var detailsToUpdateOnDb = new IpAddressDetails()
            {
                City = "NEW CITY",
                ContinentCode = "Code",
                ContinentName = "ConnectionName",
                CountryCode = "ContryCode",
                CountryName = "CountryName",
                DatabaseId = new MongoDB.Bson.ObjectId(),
                Hostname = "hostName",
                Id = 1,
                Ip = "127.0.0.1"
            };

            var mockAddressDetailsRepository = new Mock<IIpAddressDetailsRepository>();

            mockAddressDetailsRepository.Setup(m => m.GetById(It.IsAny<long>())).ReturnsAsync(iIpAddressDetailsFromDb);

            mockAddressDetailsRepository.Setup(m => m.Update(It.IsAny<IpAddressDetails>())).Callback(() => iIpAddressDetailsFromDb.City = detailsToUpdateOnDb.City).ReturnsAsync(true);

            var addressDetailsService = new IpAddressDetailsService(mockAddressDetailsRepository.Object);

            //Action

            var result = await addressDetailsService.UpdateGeoLocationData(detailsToUpdateOnDb);

            //Assert

            mockAddressDetailsRepository.Verify(m => m.GetById(It.IsAny<long>()), Times.Once);

            Assert.AreEqual(iIpAddressDetailsFromDb.City, detailsToUpdateOnDb.City);

            Assert.IsTrue(result);

        }

        [Test]
        public void UpdateGeoLocationData_ObjectDoesNotExist_ExceptionIsRaised()
        {
            //Arrange

            var mockAddressDetailsRepository = new Mock<IIpAddressDetailsRepository>();

            mockAddressDetailsRepository.Setup(m => m.GetById(It.IsAny<long>())).ReturnsAsync(() => null);

            var addressDetailsService = new IpAddressDetailsService(mockAddressDetailsRepository.Object);

            //Action && Assert

            Assert.ThrowsAsync<InvalidOperationException>(() => addressDetailsService.UpdateGeoLocationData(new IpAddressDetails()));
        }

        [Test]
        public async Task GetAllGeoLocationData_ObjectsExistInDatabase_AllObjectsAreReturned()
        {
            //Arrange
            var objects = new List<IpAddressDetails>()
            {
                new IpAddressDetails()
                {
                    City = "City",
                    ContinentCode = "Code",
                    ContinentName = "ConnectionName",
                    CountryCode = "ContryCode FROM DB",
                    CountryName = "CountryName FROM DB",
                    DatabaseId = new MongoDB.Bson.ObjectId(),
                    Hostname = "FROM DB",
                    Id = 1,
                    Ip = "127.0.0.1"
                },
                 new IpAddressDetails()
                {
                    City = "NEW CITY",
                    ContinentCode = "Code 123 ",
                    ContinentName = "ConnectionName asdas",
                    CountryCode = "ContryCode 123",
                    CountryName = "CountryName asd12",
                    DatabaseId = new MongoDB.Bson.ObjectId(),
                    Hostname = "hostName vxc231",
                    Id = 1,
                    Ip = "127.0.0.1"
                },
            };

            var mockAddressDetailsRepository = new Mock<IIpAddressDetailsRepository>();

            mockAddressDetailsRepository.Setup(m => m.GetAll()).ReturnsAsync(objects);

            var addressDetailsService = new IpAddressDetailsService(mockAddressDetailsRepository.Object);

            //Action

            var result = await addressDetailsService.GetAllGeoLocationData();

            //Assert

            mockAddressDetailsRepository.Verify(m => m.GetAll(), Times.Once);

            Assert.IsNotEmpty(result);

            Assert.AreEqual(2, result.ToList().Count);

        }
    }
}