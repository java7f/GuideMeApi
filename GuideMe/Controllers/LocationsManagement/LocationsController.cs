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
    [Authorize]
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

        [HttpGet("location/all/{email}")]
        public IActionResult GetAllLocations(string email)
        {
            var experiences = _locationsService.GetLocations(email).ToList();

            if (experiences != null) return Ok(experiences);
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
        #endregion
    }
}
