using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLocation.BL.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace GeoLocation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoLocationsController : ControllerBase
    {
        private readonly ILogger<GeoLocationsController> _logger;

        private readonly IIpAddressDetailsService _ipAddressService;

        private readonly IIpStackService _ipStackService;

        public GeoLocationsController(ILogger<GeoLocationsController> logger,
            IIpAddressDetailsService ipAddressService,
            IIpStackService ipStackService)
        {
            _logger = logger;
            _ipAddressService = ipAddressService;
            _ipStackService = ipStackService;
        }

        // GET api/geoLocations/
        [HttpPost]
        public async Task<ActionResult> GetAllIpAddresses()
        {
            return Ok(await _ipAddressService.GetAllIpAddresses());
        }

        // POST api/geoLocations/
        [HttpPost]
        public ActionResult<IpAddressDetails> SearchGeoLocation([FromBody] string ipAddress)
        {
            var ipAddressDetails = _ipStackService.GetIpAddressDetails(ipAddress);

            return Ok(ipAddressDetails);
        }
    }
}
