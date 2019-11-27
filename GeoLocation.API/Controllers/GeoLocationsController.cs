using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GeoLocation.API.Dtos;
using GeoLocation.API.Queries;
using GeoLocation.BL.Services.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace GeoLocation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeoLocationsController : ControllerBase
    {
        private readonly IIpAddressDetailsService _ipAddressService;

        private readonly IIpStackService _ipStackService;

        private readonly IMapper _mapper;

        public GeoLocationsController(
            IIpAddressDetailsService ipAddressService,
            IIpStackService ipStackService,
            IMapper mapper
            )
        {
            _ipAddressService = ipAddressService;
            _ipStackService = ipStackService;
            _mapper = mapper;
        }

        // GET api/geoLocations/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IpAddressDetails>>> GetAllStoredIpAddresses()
        {
            try
            {
                var geoLocationData = await _ipAddressService.GetAllGeoLocationData();

                return Ok(_mapper.Map<IEnumerable<IpAddressDetailsDto>>(geoLocationData));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/geoLocations/id
        [HttpGet("id")]
        public async Task<ActionResult<IpAddressDetails>> GetIpAddressDetails(long id)
        {
            try
            {
                var ipAddressDetails = await _ipAddressService.GetGeoLocationDataById(id);

                if (ipAddressDetails == null)
                    return NotFound();

                return Ok(_mapper.Map<IpAddressDetailsDto>(ipAddressDetails));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // POST api/geoLocations/
        [HttpPost]
        public async Task<ActionResult<IpAddressDetails>> AddGeoLocationData([FromBody] IpAddressQuery ipAddressQuery)
        {
            try
            {
                var ipAddressDetails = _ipStackService.GetIpAddressDetails(
                                        ipAddressQuery.Address,
                                        fields: null,   // this ensures that all data will be provided from ipStack API
                                        ipAddressQuery.IncludeHostName,
                                        ipAddressQuery.IncludeSecurity);


                await _ipAddressService.AddGeoLocationData(ipAddressDetails);

                return Ok(_mapper.Map<IpAddressDetailsDto>(ipAddressDetails));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET delete/geoLocations/id
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteGeoLocationData(long id)
        {
            try
            {
                await _ipAddressService.DeleteGeoLocationData(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGeoLocationData([FromBody] IpAddressDetailsDto ipAddressDetailsDto)
        {
            try
            {
                await _ipAddressService.UpdateGeoLocationData(_mapper.Map<IpAddressDetails>(ipAddressDetailsDto));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
