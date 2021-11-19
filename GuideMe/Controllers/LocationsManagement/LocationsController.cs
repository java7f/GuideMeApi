using GuideMe.Models.Locations;
using GuideMe.Services;
using GuideMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMe.Controllers
{
    [Route(Util.Route)]
    //[Authorize(AuthenticationSchemes = "Web, Mobile")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationsService _locationsService;

        public LocationsController(LocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        #region Locations
        [HttpGet("location/{locationId}")]
        public IActionResult GetLocation(string locationId)
        {
            var location = _locationsService.Get(locationId);

            if (location != null) return Ok(location);
            else return NotFound();
        }

        [HttpGet("location/all/{userId}")]
        public IActionResult GetAllLocations(string userId)
        {
            var experiences = _locationsService.GetLocations(userId).ToList();

            if (experiences != null) return Ok(experiences);
            else return NotFound();
        }
        
        [HttpGet("location/all")]
        public IActionResult GetAllLocationsForMobile()
        {
            var experiences = _locationsService.GetLocations().ToList();

            if (experiences != null) return Ok(experiences);
            else return NotFound();
        }

        [HttpGet("location/search/{location}")]
        public IActionResult GetSearch(string location)
        {
            location = location.ToLower();
            var locations = _locationsService.GetLocations()
                .Where(locationInfo => locationInfo.Address.City.ToLower().Contains(location) || locationInfo.Address.Country.ToLower().Contains(location)).ToList();

            if (locations != null) return Ok(locations);
            else return NotFound();
        }

        [HttpPost("location")]
        public async Task<IActionResult> InsertLocation([FromBody] Location location)
        {
            try
            {
                await _locationsService.InsertLocation(location);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("location")]
        public async Task<IActionResult> UpdateLocation([FromBody] Location location)
        {
            try
            {
                await _locationsService.UpdateLocation(location);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("location/{locationId}")]
        public async Task<IActionResult> DeleteLocation(string locationId)
        {
            try
            {
                await _locationsService.DeleteLocation(locationId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Audioguides
        [HttpGet("audioguide/{locationId}")]
        public IActionResult GetAllAudioguideForLocation(string locationId)
        {
            var audioguides = _locationsService.GetAudioguides(locationId).ToList();

            if (audioguides != null) return Ok(audioguides);
            else return NotFound();
        }

        [HttpPost("audioguide")]
        public async Task<IActionResult> InsertAudioguide([FromBody] Audioguide audioguide)
        {
            try
            {
                await _locationsService.UploadAudiofileForLocation(audioguide);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("audioguide/{audioguideId}")]
        public async Task<IActionResult> DeleteAudioguide(string audioguideId)
        {
            try
            {
                await _locationsService.DeleteAudioguide(audioguideId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("audioguide")]
        public async Task<IActionResult> UpdateAudioguide([FromBody] Audioguide audioguide)
        {
            try
            {
                await _locationsService.UpdateAudioguide(audioguide);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("audioguide/getAudioguide/{audioguideId}")]
        public IActionResult GetAudioguide(string audioguideId)
        {
            var audioguide = _locationsService.GetAudioguide(audioguideId);

            if (audioguide != null) return Ok(audioguide);
            else return NotFound();
        }

        [HttpPost("audioguide/getProximityAudioguides")]
        public IActionResult GetProximityAudioguides([FromBody] List<string> beaconIds)
        {
            var audioguides = _locationsService.GetProximityAudioguides(beaconIds);

            if (audioguides != null) return Ok(audioguides);
            else return NotFound();
        }
        #endregion
    }
}
